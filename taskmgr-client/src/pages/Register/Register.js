import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { register as registerService } from '../../services/authService';
import AuthCard from '../../components/AuthCard';

export default function Register() {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirm, setConfirm] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const onSubmit = async (e) => {
    e.preventDefault();
    setError('');
    if (password !== confirm) {
      setError('Passwords do not match');
      return;
    }
    setLoading(true);
    try {
      await registerService(username, password, email);
      // Якщо токен не збережено, показати помилку
      if (!localStorage.getItem('auth_token')) {
        setError('Register failed');
      } else {
        navigate('/');
      }
    } catch (err) {
      setError(err.message || 'Register failed');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
      <AuthCard
        title="Create account"
        subtitle="Join TaskMgr — organise your work"
        footerText="Already have an account?"
        footerLinkTo="/login"
        footerLinkText="Sign in"
      >
        <form className="auth-form" onSubmit={onSubmit}>
          <input
            className="auth-input"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
            placeholder="Username"
          />
          <input
            className="auth-input"
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
            placeholder="you@example.com"
          />
          <input
            className="auth-input"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
            placeholder="Create password"
          />
          <input
            className="auth-input"
            type="password"
            value={confirm}
            onChange={(e) => setConfirm(e.target.value)}
            required
            placeholder="Repeat password"
          />
          {error && <div className="auth-error">{error}</div>}
          <button className="auth-button" type="submit" disabled={loading}>
            {loading ? 'Creating…' : 'Create account'}
          </button>
        </form>
      </AuthCard>
    </div>
  );
}
