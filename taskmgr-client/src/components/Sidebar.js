import React from "react";

export default function Sidebar({ open, onClose }) {
  return (
    <>
      {/* Overlay */}
      <div
        onClick={onClose}
        style={{
          display: open ? "block" : "none",
          position: "fixed",
          top: 0,
          left: 0,
          width: "100vw",
          height: "100vh",
          background: "rgba(30,41,59,0.18)",
          zIndex: 1200,
        }}
      />
      {/* Sidebar */}
      <div
        style={{
          position: "fixed",
          top: 0,
          left: 0,
          height: "100vh",
          width: 270,
          background: "#fff",
          boxShadow: "2px 0 16px rgba(0,0,0,0.08)",
          zIndex: 1300,
          transform: open ? "translateX(0)" : "translateX(-100%)",
          transition: "transform 0.22s cubic-bezier(.4,1,.4,1)",
          display: "flex",
          flexDirection: "column",
          justifyContent: "space-between",
        }}
      >
        <div>
          <div style={{ padding: "28px 28px 18px 28px", fontWeight: 700, fontSize: 22, color: "#2563eb" }}>
            Menu
          </div>
          <nav style={{ display: "flex", flexDirection: "column", gap: 6, padding: "0 18px" }}>
            <button
              style={{
                background: "none",
                border: "none",
                color: "#222",
                fontSize: 18,
                fontWeight: 500,
                textAlign: "left",
                padding: "12px 10px",
                borderRadius: 6,
                cursor: "pointer",
                transition: "background 0.15s",
              }}
            >
              Tasks
            </button>
            <button
              style={{
                background: "none",
                border: "none",
                color: "#222",
                fontSize: 18,
                fontWeight: 500,
                textAlign: "left",
                padding: "12px 10px",
                borderRadius: 6,
                cursor: "pointer",
                transition: "background 0.15s",
              }}
            >
              Routines
            </button>
            <button
              style={{
                background: "none",
                border: "none",
                color: "#222",
                fontSize: 18,
                fontWeight: 500,
                textAlign: "left",
                padding: "12px 10px",
                borderRadius: 6,
                cursor: "pointer",
                transition: "background 0.15s",
              }}
            >
              Targets
            </button>
          </nav>
        </div>
        <div style={{ padding: "18px 28px", borderTop: "1px solid #f1f1f1" }}>
          <button
            style={{
              background: "none",
              border: "none",
              color: "#888",
              fontSize: 16,
              fontWeight: 500,
              textAlign: "left",
              padding: "8px 0",
              borderRadius: 6,
              cursor: "pointer",
              width: "100%",
            }}
          >
            About
          </button>
        </div>
      </div>
    </>
  );
}
