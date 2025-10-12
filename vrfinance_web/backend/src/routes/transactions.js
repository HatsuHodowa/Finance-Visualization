import express from "express";
import { pool } from "../db.js";

const router = express.Router();

// GET /api/transactions
router.get("/", async (req, res) => {
    try {
        const result = await pool.query("SELECT * FROM transactions ORDER BY date ASC");
        res.json(result.rows);
    } catch (err) {
        console.error(err);
        res.status(500).send("Server error");
    }
});

export default router;
