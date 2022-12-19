import { Button } from '@mui/material';
import Container from '@mui/material/Container';
import * as React from 'react';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from "react-router-dom";
import { API_ROOT } from '../../env';


const AppointmentDataContainer = () => {

    const navigate = useNavigate();
    const [appointment, setAppointment] = useState();
    let { id } = useParams();

    const fetchDataAppointment = async () => {
        let path = `${API_ROOT}/Appointments/${id}`
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
        const res = await fetch(`${API_ROOT}/Appointments/${appointment.appointmentId}`, {
            method: 'DELETE',
            mode: 'cors'
        });

        if (res.ok) {
            navigate("/me");
            return;
        }
    }

    useEffect(() => {
        document.body.style.backgroundColor = '#ebf6fc';

        fetchDataAppointment();

    }, [navigate])

    if (appointment)
        return (
            <div>
                {
                    <>
                        <Container style={{ width: '100%', height: '5rem', padding: '1rem', backgroundColor: "#8fc3e3" }}>
                            <h1>{appointment.title}</h1>
                        </Container>

                        <Container style={{ width: '100%', padding: '1rem', backgroundColor: "#8fc3e3" }}>
                            <h3>Created on: {appointment.dueDate}</h3>
                            <h3>Created by: {appointment.appointer}</h3>
                            <h3>Appointed to: {appointment.appointee}</h3>
                            <h3>Description: {appointment.description}</h3>
                            <Button variant="contained" style={{ margin: '0 auto 0 1rem', border: '2px solid', color: 'red' }} onClick={cancelAppointment}>Cancel</Button>
                        </Container>
                    </>
                }
            </div>
        )
}

export default AppointmentDataContainer;