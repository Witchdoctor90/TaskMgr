import { NavLink } from 'react-router-dom';
import { ReactComponent as Refresh } from '../static/icons/refresh.svg';
import { ReactComponent as Settings } from '../static/icons/settings.svg';
import { ReactComponent as UserIcon } from '../static/icons/user.svg';



const LoggedInHeader = () => (
    <header style={styles.header}>
        <div className='left-section' style={styles.leftSection}>
            <div className='burger-menu'>
                <span style={{ fontSize: '2rem', cursor: 'pointer', color:'#000' }}>&#9776;</span>
            </div>
            <NavLink to="/" className="logo" style={styles.logo}>
                <h1>TaskMgr</h1>
            </NavLink>
        </div>
        <div className='center-section' style={styles.centerSection}>
            <div className='search-container' style={styles.searchContainer}>
                <input style={styles.searchBar} className='search-bar' type="text" placeholder="Search..."/>
            </div>
        </div>
        <div className='right-section' style={styles.rightSection}>
            <div className='nav-buttons'>
                <NavLink className='nav-button' style={styles.navButton}>
                    <Refresh width='16px' height='16px'/>
                </NavLink>
                <NavLink className='nav-button' style={styles.navButton}>
                    <Settings width='16px' height='16px'/>
                </NavLink>
                <NavLink className='nav-button' style={styles.navButton}>3</NavLink>
            </div>
            <NavLink className='user-icon' style={styles.navButton}>
                <UserIcon width='20px' height='20px'/>
            </NavLink>
        </div>
    </header>
);

const styles = {
    header: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'space-between',
        padding: '0 1rem',
        height: '60px',
        background: '#F4F6F9',
        color: '#fff',
        boxShadow: '0 2px 4px rgba(0,0,0,0.05)'
    },
    leftSection:{
        display: 'flex',
        alignItems: 'center',
        gap: '1rem',
        flexDirection: 'row',
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
    searchBar:{
        padding: '0.5rem 1rem',
        borderRadius: '1.5rem',
        border: '0px solid #ccc',
        width: '100%',
    },
    searchContainer:
    {
        width: '100%',
    },
    centerSection:{
        width: '33%'
    }
};

export default LoggedInHeader;