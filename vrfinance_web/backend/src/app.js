import express from "express";
import cors from "cors";
import apiRouter from "./routes/index.js";

const app = express();

app.use(cors());
app.use(express.json());

// Mount all routes under /api
app.use("/api", apiRouter);

export default app;
