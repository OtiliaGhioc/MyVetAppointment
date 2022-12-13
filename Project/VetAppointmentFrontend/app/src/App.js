import './App.css';
import { Routes, Route, BrowserRouter as Router } from "react-router-dom";
import ProfilePage from './pages/profile/ProfilePage';
import MainPage from './pages/main/MainPage';
import NotFound from './pages/not_found/NotFound';
import MedicalHistoryPage from './pages/medical_history/MedicalHistoryEntryPage';
import AppointmentPage from './pages/appointment/AppointmentPage';
import AppointmentUpdatePage from './pages/appointment/AppointmentUpdatePage'
import AppointmentCreatePage from './pages/appointment/AppointmentCreatePage';

const App = () => {
  return (
    <Router>
      <Routes>
        <Route exact path="/" element={<MainPage />} />
        <Route exact path="/me" element={<ProfilePage />} />
        <Route exact path="*" element={<NotFound />} />
        <Route exact path="/medical-history/:id" element={<MedicalHistoryPage/>}/>
        <Route exact path="/appointment/:id" element={<AppointmentPage />} />
        <Route exact path="/appointment/:id/update" element={<AppointmentUpdatePage />} />
        <Route exact path="/appointment/create" element={<AppointmentCreatePage />} />
      </Routes>
    </Router>
  );
}

export default App;
