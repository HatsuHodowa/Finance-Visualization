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
            <h1 className="text-3xl font-bold mb-6 text-[#FC9D28]">Upload Statement</h1>

            <form
                onSubmit={handleSubmit}
                className="flex flex-col gap-4 bg-[#3A0340] p-6 rounded-lg shadow-md border border-[#795A86]"
            >
                <label className="text-sm font-medium text-[#E84C48]">Statement Name</label>
                <input
                    type="text"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    placeholder="e.g. September 2025 Statement"
                    className="border border-[#795A86] rounded-md p-2 w-full focus:ring-2 focus:ring-blue-500 outline-none placeholder:text-[#865A8E]"
                />

                <label className="text-sm font-medium text-[#E84C48]">CSV File</label>
                <input
                    type="file"
                    accept=".csv"
                    onChange={(e) => setFile(e.target.files[0])}
                    className="text-[#865A8E] border border-[#795A86] rounded-md p-2 w-full cursor-pointer file:mr-4 file:py-2 file:px-4 file:rounded-md file:border-0 file:text-sm file:font-semibold file:bg-[#CA0563] file:text-white/80 hover:file:bg-[#ea0972]"
                />

                <button
                    type="submit"
                    disabled={uploading}
                    className={`px-6 py-2 rounded-md text-white font-medium transition ${
                        uploading ? "bg-gray-400 cursor-wait" : "bg-[#CA0563] hover:bg-[#ea0972]"
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
