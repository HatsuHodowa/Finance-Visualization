import React from "react";
import bg from "../assets/vaporwave_background.png";

export default function Home() {
    return (
        <main
            className="flex flex-col items-center justify-center min-h-[80vh] text-center px-4 bg-cover bg-center"
            style={{ backgroundImage: `url(${bg})` }}
        >
            <h1 className="text-4xl font-bold mb-4 text-white">Financial Reality</h1>
            <p className="text-lg text-gray-300">3D Visualizations of Personal Finance in VR</p>
        </main>
    );
}
