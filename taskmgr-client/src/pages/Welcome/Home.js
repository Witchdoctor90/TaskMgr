import React from 'react';
import { NavLink } from 'react-router-dom';
import './Home.css';

export default function Home() {
  return (
    <div className="hero-section" style={styles.heroSection}>
      <div className="hero-text"> 
        <h1 className="hero-title" style={styles.heroTitle}>TaskMgr</h1>
        <p className="hero-text" style={styles.heroText}>Your ultimate task management solution.</p>
      </div>
      <NavLink to="/login" className='get-started-button' style={styles.getStartedButton}>Get Started</NavLink>
    </div>
  );
}

const styles = {
  heroSection: {
    padding: '4rem 0rem',
    margin: '2rem 1rem',
  },
  heroTitle:{
    fontSize: '4rem',
    fontWeight: '600',
    letterSpacing: '1px',
    marginBottom: '1rem',
  },
  heroText:{
    fontSize: '1.5rem',
    color: '#555',
    marginBottom: '5rem',
  },
  getStartedButton:{
    padding: '1rem 2rem',
    backgroundColor: '#fff',
    borderRadius: '2rem',
    color: '#007bff',
    textDecoration: 'none',
    boxShadow: '0 2px 4px rgba(0,0,0,0.1)',
    fontWeight: '600',
  }

}