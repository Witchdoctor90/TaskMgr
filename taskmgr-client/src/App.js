import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Welcome from './pages/Welcome/Welcome';
import Header from './components/header/header';
import Login from './pages/Login/Login';
import Register from './pages/Register/Register';

function App() {
  return (
    <Router>
      <div className="App">
        <Header></Header>
        <Routes>
          <Route path="/welcome" element={<Welcome />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
