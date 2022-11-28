import './App.css';
import { Routes, Route, BrowserRouter as Router } from "react-router-dom";
import UserPage from './pages/user/UserPage';
import MainPage from './pages/main/MainPage';

const App = () => {
  return (
    <Router>
      <Routes>
        <Route exact path="/" element={<MainPage />} />
        <Route exact path="/user/:id" element={<UserPage />} />
      </Routes>
    </Router>
  );
}

export default App;
