import { useState, useEffect, useRef } from 'react';
import { NavLink } from 'react-router-dom';
import { ReactComponent as Refresh } from '../static/icons/refresh.svg';
import { ReactComponent as Settings } from '../static/icons/settings.svg';
import { ReactComponent as UserIcon } from '../static/icons/user.svg';
import { getUserInfo } from '../services/authService';
import Sidebar from './Sidebar';

// Мінімалістичний SVG спінер
function Spinner() {
    return (
        <span
            style={{
                display: 'inline-flex',
                width: 16,
                height: 16,
                alignItems: 'center',
                justifyContent: 'center',
            }}
        >
            <svg
                width="16"
                height="16"
                viewBox="0 0 50 50"
                style={{ display: 'block', animation: 'spin-linear 0.8s linear infinite' }}
            >
                <circle
                    cx="25"
                    cy="25"
                    r="20"
                    fill="none"
                    stroke="#2563eb"
                    strokeWidth="5"
                    strokeDasharray="31.4 94.2"
                    strokeLinecap="round"
                />
                <style>
                    {`@keyframes spin-linear { 100% { transform: rotate(360deg); } }`}
                </style>
            </svg>
        </span>
    );
}

const LoggedInHeader = ({ spinning, onSearch, searchResults = [], onSelectTask }) => {
    const [menuOpen, setMenuOpen] = useState(false);
    const [username, setUsername] = useState('');
    const [searchValue, setSearchValue] = useState('');
    const [showDropdown, setShowDropdown] = useState(false);
    const [highlightedIdx, setHighlightedIdx] = useState(-1);
    const [searchFocused, setSearchFocused] = useState(false);
    const [sidebarOpen, setSidebarOpen] = useState(false);
    const menuRef = useRef(null);
    const iconRef = useRef(null);
    const searchInputRef = useRef(null);
    const dropdownRef = useRef(null);

    useEffect(() => {
        getUserInfo().then(u => setUsername(u?.username || ''));
    }, []);

    useEffect(() => {
        if (typeof onSearch === "function") onSearch(searchValue);
    }, [searchValue, onSearch]);

    // Закриваємо меню, якщо клік поза меню
    useEffect(() => {
        if (!menuOpen) return;
        function handleClick(e) {
            if (
                menuRef.current &&
                !menuRef.current.contains(e.target) &&
                iconRef.current &&
                !iconRef.current.contains(e.target)
            ) {
                setMenuOpen(false);
            }
        }
        document.addEventListener('mousedown', handleClick);
        return () => document.removeEventListener('mousedown', handleClick);
    }, [menuOpen]);

    // Закриваємо дропдаун пошуку при кліку поза ним
    useEffect(() => {
        if (!showDropdown) return;
        function handleClick(e) {
            if (
                dropdownRef.current &&
                !dropdownRef.current.contains(e.target) &&
                searchInputRef.current &&
                !searchInputRef.current.contains(e.target)
            ) {
                setShowDropdown(false);
            }
        }
        document.addEventListener('mousedown', handleClick);
        return () => document.removeEventListener('mousedown', handleClick);
    }, [showDropdown]);

    const handleSignOut = () => {
        localStorage.removeItem('auth_token');
        window.location.reload();
    };

    // Показувати дропдаун якщо є пошук, результати і поле в фокусі
    useEffect(() => {
        if (searchFocused && searchValue && searchResults && searchResults.length > 0) {
            setShowDropdown(true);
        } else {
            setShowDropdown(false);
        }
    }, [searchValue, searchResults, searchFocused]);

    // Reset highlight when search changes or dropdown closes
    useEffect(() => {
        setHighlightedIdx(searchResults.length > 0 ? 0 : -1);
    }, [searchResults, showDropdown]);

    // Keyboard navigation for dropdown
    const handleSearchKeyDown = (e) => {
        if (!showDropdown || !searchResults.length) return;
        if (e.key === "ArrowDown") {
            e.preventDefault();
            setHighlightedIdx(idx => Math.min(idx + 1, searchResults.length - 1));
        } else if (e.key === "ArrowUp") {
            e.preventDefault();
            setHighlightedIdx(idx => Math.max(idx - 1, 0));
        } else if (e.key === "Enter") {
            if (highlightedIdx >= 0 && searchResults[highlightedIdx]) {
                if (onSelectTask) onSelectTask(searchResults[highlightedIdx]);
                setShowDropdown(false);
            }
        }
    };

    return (
        <>
            <Sidebar open={sidebarOpen} onClose={() => setSidebarOpen(false)} />
            <header style={styles.header}>
                <div className='left-section' style={styles.leftSection}>
                    <div className='burger-menu'>
                        <span
                            style={{ fontSize: '2rem', cursor: 'pointer', color:'#000' }}
                            onClick={() => setSidebarOpen(true)}
                        >
                            &#9776;
                        </span>
                    </div>
                    <NavLink to="/" className="logo" style={styles.logo}>
                        <h1>TaskMgr</h1>
                    </NavLink>
                </div>
                <div className='center-section' style={styles.centerSection}>
                    <div className='search-container' style={styles.searchContainer}>
                        <input
                            ref={searchInputRef}
                            style={styles.searchBar}
                            className='search-bar'
                            type="text"
                            placeholder="Search..."
                            value={searchValue}
                            onChange={e => setSearchValue(e.target.value)}
                            onFocus={() => setSearchFocused(true)}
                            onBlur={() => setSearchFocused(false)}
                            onKeyDown={handleSearchKeyDown}
                            autoComplete="off"
                        />
                        {showDropdown && (
                            <div
                                ref={dropdownRef}
                                style={{
                                    position: 'absolute',
                                    top: 'calc(100% + 4px)',
                                    left: 0,
                                    width: '100%',
                                    background: '#fff',
                                    border: '1px solid #e5e7eb',
                                    borderRadius: 8,
                                    boxShadow: '0 4px 16px rgba(0,0,0,0.08)',
                                    zIndex: 20,
                                    maxHeight: 320,
                                    overflowY: 'auto',
                                }}
                            >
                                {searchResults.map((task, idx) => (
                                    <div
                                        key={task.id}
                                        style={{
                                            padding: '10px 18px',
                                            cursor: 'pointer',
                                            borderBottom: '1px solid #f3f4f6',
                                            fontSize: 15,
                                            color: '#222',
                                            background: highlightedIdx === idx ? '#e0e7ff' : '#fff',
                                            transition: 'background 0.15s',
                                            textAlign: 'left',
                                        }}
                                        onMouseDown={e => {
                                            e.preventDefault();
                                            if (onSelectTask) onSelectTask(task);
                                            setShowDropdown(false);
                                        }}
                                        onMouseEnter={() => setHighlightedIdx(idx)}
                                    >
                                        {task.title || "Untitled"}
                                    </div>
                                ))}
                            </div>
                        )}
                    </div>
                </div>
                <div className='right-section' style={styles.rightSection}>
                    <div className='nav-buttons'>
                        <NavLink className='nav-button' style={styles.navButton}>
                            {spinning ? <Spinner /> : <Refresh width='16px' height='16px' />}
                        </NavLink>
                        <NavLink className='nav-button' style={styles.navButton}>
                            <Settings width='16px' height='16px'/>
                        </NavLink>
                        <NavLink className='nav-button' style={styles.navButton}>3</NavLink>
                    </div>
                    <div
                        className='user-icon'
                        style={{ ...styles.navButton, position: 'relative' }}
                        ref={iconRef}
                        onMouseEnter={() => setMenuOpen(true)}
                        onMouseLeave={() => setTimeout(() => setMenuOpen(false), 120)}
                    >
                        <UserIcon width='20px' height='20px'/>
                        {menuOpen && (
                            <div
                                ref={menuRef}
                                onMouseEnter={() => setMenuOpen(true)}
                                onMouseLeave={() => setMenuOpen(false)}
                                style={{
                                    position: 'absolute',
                                    top: 'calc(100% + 8px)',
                                    right: 0,
                                    minWidth: 160,
                                    background: '#fff',
                                    border: '1px solid #e5e7eb',
                                    borderRadius: 8,
                                    boxShadow: '0 4px 16px rgba(0,0,0,0.08)',
                                    padding: '12px 18px',
                                    zIndex: 10,
                                    display: 'flex',
                                    flexDirection: 'column',
                                    alignItems: 'flex-start',
                                    gap: 8,
                                }}>
                                <div style={{ fontWeight: 600, color: '#222', marginBottom: 6 }}>
                                    {username ? username : 'User'}
                                </div>
                                <button
                                    onClick={handleSignOut}
                                    style={{
                                        background: '#f3f4f6',
                                        border: '1px solid #e5e7eb',
                                        color: '#dc2626',
                                        borderRadius: 6,
                                        padding: '6px 16px',
                                        fontWeight: 600,
                                        cursor: 'pointer',
                                        fontSize: 15,
                                        width: '100%',
                                    }}
                                >
                                    Sign out
                                </button>
                            </div>
                        )}
                    </div>
                </div>
            </header>
        </>
    );
};

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
    searchContainer: {
        width: '100%',
        position: 'relative'
    },
    centerSection:{
        width: '33%'
    }
};

export default LoggedInHeader;