import express from "express";
import { pool } from "../db.js";
import multer from "multer";
import { parse } from "csv-parse";

const router = express.Router();
const upload = multer({ storage: multer.memoryStorage() });

// GET /api/statements — list all statements
router.get("/", async (req, res) => {
    try {
        const result = await pool.query("SELECT * FROM statements ORDER BY start_date DESC");
        res.json(result.rows);
    } catch (err) {
        console.error("Error fetching statements:", err);
        res.status(500).send("Server error");
    }
});

// GET /api/statements/:id/transactions - transactions for a statement
router.get("/:id/transactions", async (req, res) => {
    const { id } = req.params;

    try {
        // Verify the statement exists
        const stmt = await pool.query("SELECT * FROM statements WHERE id = $1", [id]);
        if (stmt.rows.length === 0) {
            return res.status(404).json({ error: "Statement not found" });
        }

        // Get all transactions for this statement
        const result = await pool.query(
            "SELECT * FROM transactions WHERE statement_id = $1 ORDER BY date ASC",
            [id]
        );

        res.json(result.rows);
    } catch (err) {
        console.error("Error fetching transactions for statement:", err);
        res.status(500).send("Server error");
    }
});

// GET /api/statements/:id/categories
// Returns total spending per category for a statement
router.get("/:id/categories", async (req, res) => {
    const { id } = req.params;

    try {
        // Check if statement exists
        const stmt = await pool.query("SELECT * FROM statements WHERE id = $1", [id]);
        if (stmt.rows.length === 0) {
            return res.status(404).json({ error: "Statement not found" });
        }

        // Group transactions by category and sum amounts
        const result = await pool.query(
            `SELECT 
           COALESCE(category, 'Uncategorized') AS category,
           ROUND(SUM(amount)::numeric, 2) AS total
         FROM transactions
         WHERE statement_id = $1
         GROUP BY category
         ORDER BY total DESC`,
            [id]
        );

        res.json(result.rows);
    } catch (err) {
        console.error("Error fetching totals per category:", err);
        res.status(500).json({ error: "Server error" });
    }
});

// POST /api/statements — create a new statement
router.post("/", async (req, res) => {
    const { name, start_date, end_date } = req.body;

    if (!name || !start_date || !end_date) {
        return res
            .status(400)
            .json({ error: "Missing required fields: name, start_date, end_date" });
    }

    try {
        const result = await pool.query(
            `INSERT INTO statements (name, start_date, end_date)
       VALUES ($1, $2, $3)
       RETURNING *`,
            [name, start_date, end_date]
        );
        res.status(201).json(result.rows[0]);
    } catch (err) {
        console.error("Error creating statement:", err);
        res.status(500).send("Server error");
    }
});

// POST /api/statements/upload
// Creates a new statement and fills it with transactions from a CSV
router.post("/upload", upload.single("file"), async (req, res) => {
    const { name } = req.body;
    const file = req.file;

    if (!file) {
        return res.status(400).json({ error: "Missing CSV file" });
    }

    if (!name) {
        return res.status(400).json({ error: "Missing required field name" });
    }

    const client = await pool.connect();
    try {
        await client.query("BEGIN");

        // Parse CSV first (before creating the statement)
        const records = [];
        const parser = parse(file.buffer.toString(), {
            columns: (header) =>
                header.map((h) =>
                    h
                        .trim()
                        .toLowerCase()
                        .replace(/\uFEFF/g, "")
                ),
            skip_empty_lines: true,
            trim: true,
            bom: true,
        });

        for await (const record of parser) {
            const { description, category, credit, debit } = record;
            const date = record.date || record["transaction date"];

            const amount =
                credit && credit.trim() !== "" ? -Math.abs(parseFloat(credit)) : parseFloat(debit);

            if (!date || !description || !amount) {
                console.warn("Skipping invalid row:", record);
                continue;
            }

            if (isNaN(amount)) {
                console.warn("Skipping row with invalid amount:", record);
                continue;
            }

            records.push({ date, description, category, amount });
        }

        if (records.length === 0) {
            await client.query("ROLLBACK");
            return res.status(400).json({ error: "No valid transactions found in CSV" });
        }

        // Compute start and end dates
        const dates = records.map((r) => new Date(r.date));
        const start_date = new Date(Math.min(...dates));
        const end_date = new Date(Math.max(...dates));

        // Create the statement
        const stmtResult = await client.query(
            `INSERT INTO statements (name, start_date, end_date)
         VALUES ($1, $2, $3)
         RETURNING id`,
            [name, start_date, end_date]
        );

        const statementId = stmtResult.rows[0].id;

        // Insert all transactions
        for (const r of records) {
            await client.query(
                `INSERT INTO transactions (date, description, amount, category, statement_id)
           VALUES ($1, $2, $3, $4, $5)`,
                [r.date, r.description, r.amount, r.category, statementId]
            );
        }

        await client.query("COMMIT");
        res.status(201).json({
            message: "Statement created successfully",
            statement_id: statementId,
            start_date,
            end_date,
        });
    } catch (err) {
        await client.query("ROLLBACK");
        console.error("Error creating statement from CSV:", err);
        res.status(500).json({ error: "Failed to create statement" });
    } finally {
        client.release();
    }
});

router.delete("/:id", async (req, res) => {
    const { id } = req.params;
    try {
        const result = await pool.query("DELETE FROM statements WHERE id = $1 RETURNING *", [id]);
        if (result.rowCount === 0) {
            return res.status(404).json({ error: "Statement not found" });
        }
        res.json({ message: "Statement deleted successfully" });
    } catch (err) {
        console.error("Error deleting statement:", err);
        res.status(500).json({ error: "Failed to delete statement" });
    }
});

export default router;
