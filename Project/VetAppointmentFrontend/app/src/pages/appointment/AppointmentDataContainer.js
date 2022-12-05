import Container from '@mui/material/Container';
import * as React from 'react';
import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from 'react';


const AppointmentDataContainer = () => {

    const navigate = useNavigate();
    const [appointment, setAppointment] = useState();
    let { id } = useParams();
    
    const fetchDataAppointment = async () => {
        let path = 'https://localhost:7116/api/Appointments/' + id
        const response = await fetch(path, {
            method: 'GET',
            mode: 'cors'
        }).then((response) => response.json())

        // if (!response.ok) {
        //     navigate("/not-found");
        //     return;
        // }

        setAppointment(response);
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
                            <h3>Created on: {appointment.dateTime}</h3>
                            <h3>Appointer: {appointment.appointerId}</h3>
                            <h3>Description: {appointment.description}</h3>
                        </Container>
                    </>
                }
            </div>
        )
}

export default AppointmentDataContainer;