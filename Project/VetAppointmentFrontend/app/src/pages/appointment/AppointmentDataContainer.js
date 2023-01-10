import { Button } from '@mui/material';
import Container from '@mui/material/Container';
import * as React from 'react';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from "react-router-dom";
import { API_ROOT } from '../../env';
import CancelAppointmentModal from './CancelAppointmentModal';
import EditAppointmentModal from './EditAppointmentModal';


const AppointmentDataContainer = () => {

    const navigate = useNavigate();
    const [appointment, setAppointment] = useState();
    const [isCancelAppointmentModalOpen, setIsCancelAppointmentModalOpen] = React.useState(false);
    const [isEditAppointmentModalOpen, setIsEditAppointmentModalOpen] = React.useState(false);
    let { id } = useParams();

    const fetchDataAppointment = async () => {
        let path = `${API_ROOT}/v1.0/Appointments/${id}`
        const response = await fetch(path, {
            method: 'GET',
            mode: 'cors'
        })

        if (!response.ok) {
            navigate("/not-found");
            return;
        }

        const json_data = await response.json();

        setAppointment(json_data);
    }

    const cancelAppointment = async () => {
        const res = await fetch(`${API_ROOT}/v1.0/Appointments/${appointment.appointmentId}`, {
            method: 'DELETE',
            mode: 'cors'
        });

        if (res.ok) {
            navigate("/appointappointmentsnts");
            return;
        }
    }

    useEffect(() => {
        document.body.style.backgroundColor = '#ebf6fc';

        fetchDataAppointment();

    }, [navigate])
    
    const openCancelAppointmentModal = () => {
        setIsCancelAppointmentModalOpen(true);
    }

    const handleCancelAppointmentModalClose = () => {
        setIsCancelAppointmentModalOpen(false);
    }

    const openEditAppointmentModal = () => {
        setIsEditAppointmentModalOpen(true);
    }

    const handleEditAppointmentModalClose = () => {
        setIsEditAppointmentModalOpen(false);
    }
    
    if (appointment)
        return (
            <div>
                {
                    <>
                        <Container style={{ width: '100%', padding: '1rem', backgroundImage: 'url(/img/dog_paws_pattern.jpg)', textShadow: '2px 2px 4px black', backgroundSize: '150%', color: 'white' }}>
                            <h1>{appointment.title}</h1>
                            <h3>Created on: {appointment.dueDate}</h3>
                            <h3>Created by: {appointment.appointer}</h3>
                            <h3>Appointed to: {appointment.appointee}</h3>
                            <h3>Description: {appointment.description}</h3>
                            <Button id=" cancel" type="button" variant="contained" style={{ margin: '0 auto 0 1rem', border: '2px solid', color: 'red' }} onClick={openCancelAppointmentModal}>Cancel</Button>
                            <Button id="edit" type="button" variant="contained" style={{ margin: '0 auto 0 1rem', border: '2px solid', color: 'green' }} onClick={openEditAppointmentModal}>Edit</Button>
                            <CancelAppointmentModal isOpen={isCancelAppointmentModalOpen} handleClose={handleCancelAppointmentModalClose} cancelAppointmentCallback={cancelAppointment}/>
                            <EditAppointmentModal 
                                isOpen={isEditAppointmentModalOpen} 
                                handleClose={handleEditAppointmentModalClose} 
                                appointmentId={id}
                                appointmentTitle={appointment.title}
                                appointmentData={appointment.dueDate}
                                appointmentDesc={appointment.description}                    
                            />
                        </Container>
                    </>
                }
            </div>
        )
}

export default AppointmentDataContainer;