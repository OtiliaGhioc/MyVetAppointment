import * as React from 'react';
import { Button, Typography } from "@mui/material";
import { Container } from "@mui/system";
import { useLocation, useNavigate } from "react-router-dom";
import { API_ROOT, BACKGROUND_COLOR } from "../../env";
import { getDocumentName } from '../../util/DocumentUtil';
import { disconnectUser, getAccessToken, getRefreshToken, makeRequestWithJWT } from "../../util/JWTUtil";
import AppointmentsList from "./AppointmentsList";
import CreateAppointmentModal from './CreateAppointmentModal';

const AppointmentsListPage = ({ locationChangeCallback }) => {
    const [appointments, setAppointments] = React.useState([]);
    const location = useLocation()
    const navigate = useNavigate()
    const [isEditAppointmentModalOpen, setIsEditAppointmentModalOpen] = React.useState(false);
    const [isCreateAppointmentModalOpen, setIsCreateAppointmentModalOpen] = React.useState(false);

    const [medicsList, setMedicsList] = React.useState([]);
    const [patientsList, setPatientsList] = React.useState([]);
    const [currentuser, setCurrentUser] = React.useState();
    const [currentusername, setCurrentUserName] = React.useState();

    const [isMedic, setIsMedic] = React.useState(false);

    React.useEffect(() => {
        document.title = getDocumentName('Appointments');
    }, []);

    React.useEffect(() => {
        locationChangeCallback(location);
    }, [location]);

    const setPageData = (dataApp, dataUser) => {
        setAppointments(dataApp.appointments);
    }

    React.useEffect(() => {
        document.body.style.backgroundColor = BACKGROUND_COLOR;

        const fetchDataPage = async () => {
            let res = await makeRequestWithJWT(
                `${API_ROOT}/v1.0/Users/me/`, {
                method: 'GET',
                mode: 'cors'
            }, {
                accessToken: getAccessToken(),
                refreshToken: getRefreshToken()
            }
            )

            if (res.status === 401) {
                disconnectUser();
                navigate('/login');
                return;
            }

            if (!res.ok) {
                navigate('/not-found');
                return;
            }

            let userJson = await res.json();
            setIsMedic(userJson.isMedic);

            res = await makeRequestWithJWT(
                `${API_ROOT}/v1.0/Users/me/appointments`, {
                method: 'GET',
                mode: 'cors'
            }, {
                accessToken: getAccessToken(),
                refreshToken: getRefreshToken()
            }
            )

            if (res.status === 401) {
                disconnectUser();
                navigate('/login');
                return;
            }

            if (!res.ok) {
                navigate('/not-found');
                return;
            }

            const jsonData = await res.json();
            setCurrentUser(jsonData.userId);
            setCurrentUserName(jsonData.username)

            if(isMedic) {
                res = await makeRequestWithJWT(
                    `${API_ROOT}/v1.0/Users/patients`, {
                    method: 'GET',
                    mode: 'cors'
                }, {
                    accessToken: getAccessToken(),
                    refreshToken: getRefreshToken()
                }
                )
                if (res.status === 401) {
                    disconnectUser();
                    navigate('/login');
                    return;
                }
                let patientsJson = await res.json();
                setPatientsList(patientsJson);
            } else {
                res = await makeRequestWithJWT(
                    `${API_ROOT}/v1.0/Users/medics`, {
                    method: 'GET',
                    mode: 'cors'
                }, {
                    accessToken: getAccessToken(),
                    refreshToken: getRefreshToken()
                }
                )
                if (res.status === 401) {
                    disconnectUser();
                    navigate('/login');
                    return;
                }
                let medicsJson = await res.json();
                setMedicsList(medicsJson);
            }
            
            setPageData(jsonData);
        }

        fetchDataPage();

    }, [navigate])

    const openCreateAppointmentModal = () => {
        setIsCreateAppointmentModalOpen(true);
    }

    const handleCreateAppointmentModalClose = () => {
        setIsCreateAppointmentModalOpen(false);
    }

    const updateAppointmentListState = (AppointmentData) => {
        setAppointments([...appointments, {
            appointer: "simona@gmail.com",
            appointmentId: "447c4299-433e-4119-829a-0ec3e55f87db",
            dueDate: "19-Jan-2023",
            dueTime: "15:30",
            title: "tirle2"
        }])
    }
    return (
        <>
        <Container style={{ width: '100%', padding: '0.5rem 1rem', margin: '0 auto 1.5rem auto', backgroundImage: 'url(/img/dog_paws_pattern.jpg)', backgroundSize: '150%', display: 'flex', flex: 'none', justifyContent: 'space-between' }}>
            <Button
                type="button"
                style={{ width: '14rem', backgroundColor: "#ffffff", color: '#186bc9', fontWeight: '600' }}
                variant="contained"
                sx={{ mt: 3, mb: 2 }}
                     onClick={openCreateAppointmentModal}
                >
                Add appointment
             </Button>
            <CreateAppointmentModal
                currentUserId={currentuser}
                currentUserName={currentusername}
                isOpen={isCreateAppointmentModalOpen}
                handleClose={handleCreateAppointmentModalClose}
                updateAppointmentList={updateAppointmentListState}
                isMedic={isMedic}
                medicsList={medicsList} 
                patientsList={patientsList} 
            />
            </Container>
            <AppointmentsList appointments={appointments}></AppointmentsList>
        </>
    )
}

export default AppointmentsListPage;