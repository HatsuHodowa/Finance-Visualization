import React, { useEffect, useState } from "react";
import { motion, AnimatePresence } from "framer-motion";
import {
    getStatements,
    getStatementTransactions,
    getStatementCategories,
    deleteStatement,
} from "../services/api";

// Helper for pretty dates
function formatDate(dateStr) {
    return new Intl.DateTimeFormat("en-US", {
        month: "long",
        day: "numeric",
        year: "numeric",
    }).format(new Date(dateStr));
}

export default function Statements() {
    const [statements, setStatements] = useState([]);
    const [openId, setOpenId] = useState(null);
    const [transactions, setTransactions] = useState({});
    const [categories, setCategories] = useState({});
    const [loadingId, setLoadingId] = useState(null);
    const [deletingId, setDeletingId] = useState(null);
    const [error, setError] = useState(null);

    useEffect(() => {
        async function fetchStatements() {
            try {
                const data = await getStatements();
                setStatements(data);
            } catch {
                setError("Failed to load statements");
            }
        }
        fetchStatements();
    }, []);

    async function toggleStatement(id) {
        if (openId === id) {
            setOpenId(null);
            return;
        }

        setOpenId(id);
        setLoadingId(id);

        try {
            // Fetch both transactions and categories in parallel
            const [txData, catData] = await Promise.all([
                transactions[id] ? Promise.resolve(transactions[id]) : getStatementTransactions(id),
                categories[id] ? Promise.resolve(categories[id]) : getStatementCategories(id),
            ]);

            setTransactions((prev) => ({ ...prev, [id]: txData }));
            setCategories((prev) => ({ ...prev, [id]: catData }));
        } catch (err) {
            console.error(err);
            setError("Failed to load statement details");
        } finally {
            setLoadingId(null);
        }
    }

    async function handleDelete(id) {
        const confirmDelete = window.confirm(
            "Are you sure you want to delete this statement and all its transactions?"
        );
        if (!confirmDelete) return;

        try {
            setDeletingId(id);
            await deleteStatement(id);
            setStatements((prev) => prev.filter((s) => s.id !== id));
            setTransactions((prev) => {
                const updated = { ...prev };
                delete updated[id];
                return updated;
            });
            setCategories((prev) => {
                const updated = { ...prev };
                delete updated[id];
                return updated;
            });
        } catch (err) {
            alert("Failed to delete statement.");
            console.error(err);
        } finally {
            setDeletingId(null);
        }
    }

    if (error) return <p className="text-center mt-10 text-red-500">{error}</p>;

    return (
        <div className="p-8">
            <h1 className="text-3xl font-bold mb-6 text-[#990457]">Statements</h1>

            <AnimatePresence>
                {statements.map((s) => (
                    <motion.div
                        key={s.id}
                        initial={{ opacity: 0, y: 10 }}
                        animate={{ opacity: 1, y: 0 }}
                        exit={{ opacity: 0, y: -10 }}
                        transition={{ duration: 0.2 }}
                        className="mb-4 border border-[#795A86] rounded-lg bg-[#3A0340] shadow-sm overflow-hidden"
                    >
                        <div className="flex justify-between items-center px-6 py-4 hover:bg-[#500449] transition">
                            <button
                                onClick={() => toggleStatement(s.id)}
                                className="flex-grow text-left"
                            >
                                <div className="font-medium text-[#FC9D28]">{s.name}</div>
                                <div className="text-sm text-gray-500">
                                    {formatDate(s.start_date)} — {formatDate(s.end_date)}
                                </div>
                            </button>

                            <div className="flex items-center gap-4">
                                <motion.button
                                    onClick={() => toggleStatement(s.id)}
                                    className="text-gray-500 text-lg focus:outline-none"
                                    animate={{ rotate: openId === s.id ? 180 : 0 }}
                                    transition={{ duration: 0.3 }}
                                >
                                    ▼
                                </motion.button>

                                <button
                                    onClick={() => handleDelete(s.id)}
                                    disabled={deletingId === s.id}
                                    className={`text-sm px-3 py-1 rounded-md transition ${
                                        deletingId === s.id
                                            ? "bg-gray-300 text-gray-700 cursor-wait"
                                            : "bg-red-500 text-white hover:bg-red-600"
                                    }`}
                                >
                                    {deletingId === s.id ? "Deleting..." : "Delete"}
                                </button>
                            </div>
                        </div>

                        {/* Expandable Section */}
                        <AnimatePresence initial={false}>
                            {openId === s.id && (
                                <motion.div
                                    key="details"
                                    initial={{ height: 0, opacity: 0 }}
                                    animate={{ height: "auto", opacity: 1 }}
                                    exit={{ height: 0, opacity: 0 }}
                                    transition={{ duration: 0.3, ease: "easeInOut" }}
                                    className="overflow-hidden px-6 pb-4"
                                >
                                    {loadingId === s.id ? (
                                        <p className="text-[#FC9D28] mt-2">Loading details...</p>
                                    ) : (
                                        <>
                                            {/* Category Summary */}
                                            {categories[s.id] && categories[s.id].length > 0 && (
                                                <div className="mt-4 mb-6">
                                                    <h2 className="text-lg font-semibold text-[#FC9D28] mb-2">
                                                        Category Summary
                                                    </h2>
                                                    <table className="min-w-full border border-[#795A86] text-sm mb-3 text-[#FC9D28]">
                                                        <thead className="bg-[#B3035C] text-[#FC9D28] border-[#795A86] border-b">
                                                            <tr>
                                                                <th className="text-left px-4 py-2">
                                                                    Category
                                                                </th>
                                                                <th className="text-right px-4 py-2">
                                                                    Total Spent
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            {categories[s.id].map((cat, i) => (
                                                                <tr
                                                                    key={i}
                                                                    className="border-b border-[#795A86] hover:bg-[#7a2384]"
                                                                >
                                                                    <td className="px-4 py-2">
                                                                        {cat.category
                                                                            ? cat.category
                                                                            : "None"}
                                                                    </td>
                                                                    <td
                                                                        className={`px-4 py-2 text-right font-medium ${
                                                                            cat.total < 0
                                                                                ? "text-green-600"
                                                                                : "text-gray-500"
                                                                        }`}
                                                                    >
                                                                        $
                                                                        {Number(cat.total).toFixed(
                                                                            2
                                                                        )}
                                                                    </td>
                                                                </tr>
                                                            ))}
                                                        </tbody>
                                                    </table>
                                                </div>
                                            )}

                                            {/* Transactions Table */}
                                            {transactions[s.id] && transactions[s.id].length > 0 ? (
                                                <div>
                                                    <h2 className="text-lg font-semibold text-[#FC9D28] mb-2">
                                                        Transactions
                                                    </h2>
                                                    <table className="min-w-full border border-[#795A86] text-sm">
                                                        <thead className="bg-[#B3035C] text-[#FC9D28] border-[#795A86] border-b">
                                                            <tr>
                                                                <th className="text-left px-4 py-2">
                                                                    Date
                                                                </th>
                                                                <th className="text-left px-4 py-2">
                                                                    Description
                                                                </th>
                                                                <th className="text-right px-4 py-2">
                                                                    Amount
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            {transactions[s.id].map((tx, i) => (
                                                                <tr
                                                                    key={i}
                                                                    className="border-b border-[#795A86] hover:bg-[#7a2384] text-[#E23746]"
                                                                >
                                                                    <td className="px-4 py-2">
                                                                        {formatDate(tx.date)}
                                                                    </td>
                                                                    <td className="px-4 py-2">
                                                                        {tx.description}
                                                                    </td>
                                                                    <td
                                                                        className={`px-4 py-2 text-right font-medium ${
                                                                            tx.amount < 0
                                                                                ? "text-green-600"
                                                                                : "text-gray-500"
                                                                        }`}
                                                                    >
                                                                        $
                                                                        {Number(tx.amount).toFixed(
                                                                            2
                                                                        )}
                                                                    </td>
                                                                </tr>
                                                            ))}
                                                        </tbody>
                                                    </table>
                                                </div>
                                            ) : (
                                                <p className="text-gray-500 mt-2">
                                                    No transactions found.
                                                </p>
                                            )}
                                        </>
                                    )}
                                </motion.div>
                            )}
                        </AnimatePresence>
                    </motion.div>
                ))}
            </AnimatePresence>

            {statements.length === 0 && (
                <p className="text-gray-500 mt-6 text-center">No statements available.</p>
            )}
        </div>
    );
}
