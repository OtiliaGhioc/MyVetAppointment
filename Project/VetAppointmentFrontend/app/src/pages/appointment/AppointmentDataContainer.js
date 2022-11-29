import Container from '@mui/material/Container';
import * as React from 'react';
import { useNavigate } from "react-router-dom";
import { useEffect } from 'react';

function createAppointmentRowEntry(title, dueDate, dueTime, appointer) {
    return {
        title,
        dueDate,
        dueTime,
        appointer,
        button: {
            isButton: true,
            text: 'View'
        }
    };
}

const AppointmentDataContainer = () => {

    const navigate = useNavigate();

    useEffect(() => {
        document.body.style.backgroundColor = '#ebf6fc';

        const fetchData = async () => {
            const res = await fetch('https://localhost:7116/api/Appointments', {
                method: 'GET',
                mode: 'cors'
            });

            // if (!res.ok) {
            //     navigate("/not-found");
            //     return;
            // }

            const json_data = await res.json();
            console.log(json_data)
        }
        fetchData();
    }, [navigate])

    const appointmentsData = [
        createAppointmentRowEntry('Appt. 1', '30-10-2022', '16:00', 'Dr. Smith')
    ]

    return (
        <>
            <Container style={{ width: '100%', height: '5rem', padding: '1rem', backgroundColor: "#8fc3e3" }}>
                <h1>{appointmentsData[0].title}</h1>
            </Container>

            <Container style={{ width: '100%', padding: '1rem', backgroundColor: "#8fc3e3" }}>
                <h3>Due Date: {appointmentsData[0].dueDate}</h3>
                <h3>Due Time: {appointmentsData[0].dueTime}</h3>
                <h3>Appointer: {appointmentsData[0].appointer}</h3>
            </Container>
        </>
    )
}

export default AppointmentDataContainer;