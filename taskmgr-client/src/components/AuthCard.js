import React from 'react';
import './AuthCard.css';

export default function AuthCard({ title, subtitle, footerText, footerLinkTo, footerLinkText, children }) {
  return (
    <div className="auth-card" style={{ 
      display: 'flex', 
      flexDirection: 'column', 
      alignItems: 'center', 
      justifyContent: 'center', 
      height: 'auto', 
      width: '400px', // Встановлюємо ширину для карточки
      padding: '20px', // Додаємо відступи
      border: '1px solid #ccc', // Додаємо рамку
      borderRadius: '8px', // Закруглюємо кути
      boxShadow: '0 2px 10px rgba(0, 0, 0, 0.1)', // Додаємо тінь
      backgroundColor: '#fff' // Фоновий колір
    }}>
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
