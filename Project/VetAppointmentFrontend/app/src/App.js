import './App.css';
import { Routes, Route, BrowserRouter as Router } from "react-router-dom";
import ProfilePage from './pages/profile/ProfilePage';
import MainPage from './pages/main/MainPage';
import NotFound from './pages/not_found/NotFound';
import MedicalHistoryPage from './pages/medical_history/MedicalHistoryEntryPage';
import AppointmentPage from './pages/appointment/AppointmentPage';
import LoginPage from './pages/login/LoginPage';
import RegisterPage from './pages/register/RegisterPage';

const App = () => {
  return (
    <Router>
      <Routes>
        <Route exact path="/" element={<MainPage />} />
        <Route exact path="/me" element={<ProfilePage />} />
        <Route exact path="/login" element={<LoginPage />} />
        <Route exact path="/register" element={<RegisterPage />} />
        <Route exact path="/medical-history/:id" element={<MedicalHistoryPage/>}/>
        <Route exact path="/appointment/:id" element={<AppointmentPage />} />
        <Route exact path="*" element={<NotFound />} />
      </Routes>
    </Router>
  );
}

export default App;
