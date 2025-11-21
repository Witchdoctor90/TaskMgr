import React from 'react';
import './AuthCard.css';

export default function AuthCard({ title, subtitle, footerText, footerLinkTo, footerLinkText, children }) {
  return (
    <div className="auth-card">
      <h2 className="auth-title">{title}</h2>
      <p className="auth-subtitle">{subtitle}</p>
      <div className="auth-content">
        {children}
      </div>
      <div className="auth-footer">
        {footerText} <a href={footerLinkTo}>{footerLinkText}</a>
      </div>
    </div>
  );
}
