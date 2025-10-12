import React, { useEffect, useState } from "react";
import { getTransactions } from "../services/api";

export default function Transactions() {
    const [transactions, setTransactions] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        async function fetchData() {
            try {
                const data = await getTransactions();
                setTransactions(data);
            } catch (err) {
                setError("Failed to load transactions");
            } finally {
                setLoading(false);
            }
        }
        fetchData();
    }, []);

    if (loading) return <p className="text-center mt-10 text-gray-500">Loading...</p>;
    if (error) return <p className="text-center mt-10 text-red-500">{error}</p>;

    return (
        <div className="p-8">
            <h1 className="text-3xl font-bold mb-6 text-[#990457]">Transactions</h1>
            <div className="overflow-x-auto">
                <table className="min-w-full bg-[#3A0340] border border-[#795A86] rounded-lg shadow-sm">
                    <thead className="bg-[#CA0563] text-[#FC9D28] border-[#795A86]">
                        <tr>
                            <th className="text-left px-6 py-3">Date</th>
                            <th className="text-left px-6 py-3">Description</th>
                            <th className="text-left px-6 py-3">Category</th>
                            <th className="text-right px-6 py-3">Amount</th>
                        </tr>
                    </thead>
                    <tbody>
                        {transactions.map((tx, i) => (
                            <tr key={i} className="border-b hover:bg-[#7a2384] text-[#E23746]">
                                <td className="px-6 py-3">
                                    {new Intl.DateTimeFormat("en-US", {
                                        month: "long",
                                        day: "numeric",
                                        year: "numeric",
                                    }).format(new Date(tx.date))}
                                </td>
                                <td className="px-6 py-3">{tx.description}</td>
                                <td className="px-6 py-3">{tx.category ? tx.category : "None"}</td>
                                <td
                                    className={`px-6 py-3 text-right font-medium ${
                                        tx.amount < 0 ? "text-green-600" : "text-gray-500"
                                    }`}
                                >
                                    ${Number(tx.amount).toFixed(2)}
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}
