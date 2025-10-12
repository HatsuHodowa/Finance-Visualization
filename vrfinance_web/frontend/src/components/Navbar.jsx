import React from "react";

export default function Navbar() {
    return (
        <nav className="flex justify-between items-center px-8 py-4 bg-white shadow-sm border-b border-gray-200">
            <div className="text-xl font-bold text-gray-800">Financial Reality</div>
            <div className="flex gap-6 text-gray-600">
                <a href="/" className="hover:text-gray-900 transition">
                    Home
                </a>
                <a href="/transactions" className="hover:text-gray-900 transition">
                    Transactions
                </a>
                <a href="/statements" className="hover:text-gray-900 transition">
                    Statements
                </a>
                <a href="/upload-statement" className="hover:text-gray-900 transition">
                    Upload Statement
                </a>
            </div>
        </nav>
    );
}
