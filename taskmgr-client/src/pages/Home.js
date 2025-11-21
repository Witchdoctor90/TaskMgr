import React from 'react';
import { Link } from 'react-router-dom';
import './Home.css';

const Feature = ({ title, text }) => (
  <div className="home-feature">
    <h3>{title}</h3>
    <p>{text}</p>
  </div>
);

export default function Home() {
  return (
    <main className="home-container">
      <header className="home-hero">
        <h1>TaskMgr</h1>
        <p className="home-subtitle">Простий менеджер задач — плануй, відстежуй і досягай цілей.</p>
        <Link to="/login" className="home-cta">Get Started</Link>
      </header>

      <section className="home-presentation">
        <h2>Що вміє проєкт</h2>
        <div className="home-features">
          <Feature title="Завдання" text="Створюй, редагуй і відстежуй завдання з дедлайнами." />
          <Feature title="Рутини" text="Автоматизуй повторювані дії та налаштовуй нагадування." />
          <Feature title="Цілі" text="Групуй завдання по цілям та відстежуй прогрес." />
        </div>

        <div className="home-showcase">
          <div className="card">
            <h4>Швидкий старт</h4>
            <p>Реєструйся або заходь — отримуєш токен і доступ до API.</p>
          </div>
          <div className="card">
            <h4>API-ready</h4>
            <p>Використовуй OpenAPI-ендпоінти для інтеграцій.</p>
          </div>
          <div className="card">
            <h4>Static-ready</h4>
            <p>Підходить для розгортання як Static Web App.</p>
          </div>
        </div>
      </section>

      <footer className="home-footer">
        <small>© TaskMgr — простий інструмент для продуктивності</small>
      </footer>
    </main>
  );
}
