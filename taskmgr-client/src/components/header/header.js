import { NavLink } from 'react-router-dom';

const Header = () => (
    <header style={styles.header}>
        <div className='left-section'>
            <div className='burger-menu'>

            </div>
            <NavLink to="/" className="logo" style={styles.logo}>
                <h1>TaskMgr</h1>
            </NavLink>
        </div>
        <div className='right-section' style={styles.rightSection}>
            <div className='nav-buttons'>
                <NavLink to="#" className='nav-button' style={styles.navButton}>Pricing</NavLink>
                <NavLink to="#" className='nav-button' style={styles.navButton}>Features</NavLink>
            </div>
            <NavLink to='/login' className='login-button' style={styles.loginButton}>Sign In</NavLink>
        </div>
    </header>
);

const styles = {
    header: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'space-between',
        padding: '0 2rem',
        height: '60px',
        background: '#fff',
        color: '#fff',
        boxShadow: '0 2px 4px rgba(0,0,0,0.05)'
    },
    logo: {
        fontSize: '1rem',
        fontWeight: 'bold',
        color: '#000',
        textDecoration: 'none',
    },
    rightSection:{
        display: 'flex',
        alignItems: 'center',
        gap: '1rem',
        flexDirection: 'row',
    },
    navButton: {
        padding: '1.5rem',
        color: '#000',
        textDecoration: 'none',
    },
    loginButton:{ 
        padding: '0.7rem 1.2rem',
        backgroundColor: '#007bff',
        borderRadius: '1.2rem',
        color: '#fff',
        textDecoration: 'none',
    }
};

export default Header;