import express from "express";
import pkg from "pg";

const { Pool } = pkg;
const app = express();
app.use(express.json());

// Connect to PostgreSQL
const pool = new Pool({
    user: process.env.PGUSER || "postgres",
    host: "localhost",
    database: "vrfinance",
    password: process.env.PGPASSWORD || "postgres",
    port: 5432,
});

// Example route: get all transactions
app.get("/transactions", async (req, res) => {
    try {
        const result = await pool.query("SELECT * FROM transactions");
        res.json(result.rows);
    } catch (err) {
        console.error(err);
        res.status(500).send("Server error");
    }
});

// Example route: add a transaction
app.post("/transactions", async (req, res) => {
    const { date, amount, description, statement_id } = req.body;
    try {
        const result = await pool.query(
            "INSERT INTO transactions (date, amount, description, statement_id) VALUES ($1, $2, $3, $4) RETURNING *",
            [date, amount, description, statement_id]
        );
        res.status(201).json(result.rows[0]);
    } catch (err) {
        console.error(err);
        res.status(500).send("Server error");
    }
});

const PORT = 3000;
app.listen(PORT, () => console.log(`Server running on http://localhost:${PORT}`));
