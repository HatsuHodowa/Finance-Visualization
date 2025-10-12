import express from "express";
import transactionsRouter from "./transactions.js";
import statementsRouter from "./statements.js";

const router = express.Router();

// Mount transactions router under /transactions
router.use("/transactions", transactionsRouter);
router.use("/statements", statementsRouter);

export default router;
