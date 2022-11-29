import './App.css';
import { Routes, Route, BrowserRouter as Router } from "react-router-dom";
import ProfilePage from './pages/profile/ProfilePage';
import MainPage from './pages/main/MainPage';
import NotFound from './pages/not_found/NotFound'
import AppointmentPage from './pages/appointment/AppointmentPage';

const App = () => {
  return (
    <Router>
      <Routes>
        <Route exact path="/" element={<MainPage />} />
        <Route exact path="/me" element={<ProfilePage />} />
        <Route exact path="*" element={<NotFound />} />
        <Route exact path="/appointment/:id" element={<AppointmentPage />} />
      </Routes>
    </Router>
  );
}

export default App;
