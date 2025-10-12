import React from "react";
import Navbar from "./components/Navbar";
import Home from "./pages/Home";
import Transactions from "./pages/Transactions";
import Statements from "./pages/Statements";
import UploadStatement from "./pages/UploadStatement";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

function App() {
    return (
        <div className="min-h-screen bg-[#20022E]">
            <Navbar />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/transactions" element={<Transactions />} />
                <Route path="/statements" element={<Statements />} />
                <Route path="/upload-statement" element={<UploadStatement />} />
            </Routes>
        </div>
    );
}

export default App;
