import React, { useState } from "react";
import { uploadStatement } from "../services/api";

export default function UploadStatement() {
    const [file, setFile] = useState(null);
    const [name, setName] = useState("");
    const [uploading, setUploading] = useState(false);
    const [message, setMessage] = useState(null);
    const [error, setError] = useState(null);

    async function handleSubmit(e) {
        e.preventDefault();
        setError(null);
        setMessage(null);

        if (!name.trim()) {
            setError("Please enter a name for the statement.");
            return;
        }
        if (!file) {
            setError("Please select a CSV file to upload.");
            return;
        }

        try {
            setUploading(true);
            await uploadStatement(file, name);
            setMessage("Statement uploaded successfully!");
            setName("");
            setFile(null);
        } catch (err) {
            console.error(err);
            setError("Failed to upload the statement.");
        } finally {
            setUploading(false);
        }
    }

    return (
        <div className="p-8 max-w-lg mx-auto">
            <h1 className="text-3xl font-bold mb-6 text-gray-800">Upload Statement</h1>

            <form
                onSubmit={handleSubmit}
                className="flex flex-col gap-4 bg-white p-6 rounded-lg shadow-md border border-gray-200"
            >
                <label className="text-sm font-medium text-gray-700">Statement Name</label>
                <input
                    type="text"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    placeholder="e.g. September 2025 Statement"
                    className="border border-gray-300 rounded-md p-2 w-full focus:ring-2 focus:ring-blue-500 outline-none"
                />

                <label className="text-sm font-medium text-gray-700">CSV File</label>
                <input
                    type="file"
                    accept=".csv"
                    onChange={(e) => setFile(e.target.files[0])}
                    className="border border-gray-300 rounded-md p-2 w-full cursor-pointer file:mr-4 file:py-2 file:px-4 file:rounded-md file:border-0 file:text-sm file:font-semibold file:bg-blue-50 file:text-blue-700 hover:file:bg-blue-100"
                />

                <button
                    type="submit"
                    disabled={uploading}
                    className={`px-6 py-2 rounded-md text-white font-medium transition ${
                        uploading ? "bg-gray-400 cursor-wait" : "bg-blue-600 hover:bg-blue-700"
                    }`}
                >
                    {uploading ? "Uploading..." : "Upload"}
                </button>

                {message && <p className="text-green-600 text-sm">{message}</p>}
                {error && <p className="text-red-600 text-sm">{error}</p>}
            </form>
        </div>
    );
}
