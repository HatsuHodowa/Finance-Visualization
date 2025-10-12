import React from "react";

export default function Navbar() {
    return (
        <nav className="flex justify-between items-center px-8 py-4 bg-[#20022E] shadow-sm border-b border-gray-200">
            <div className="text-xl font-bold text-white">Financial Reality</div>
            <div className="flex gap-6 text-white">
                <a href="/" className="hover:text-white/60 transition">
                    Home
                </a>
                <a href="/transactions" className="hover:text-white/60 transition">
                    Transactions
                </a>
                <a href="/statements" className="hover:text-white/60 transition">
                    Statements
                </a>
                <a href="/upload-statement" className="hover:text-white/60 transition">
                    Upload Statement
                </a>
            </div>
        </nav>
    );
}
