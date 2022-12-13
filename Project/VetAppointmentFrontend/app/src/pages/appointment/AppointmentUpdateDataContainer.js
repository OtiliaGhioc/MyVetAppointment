import Container from '@mui/material/Container';
import * as React from 'react';
import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from 'react';
import { API_ROOT } from '../../env';
import { Button } from '@mui/material';
import axios from 'axios';


const AppointmentUpdateDataContainer = () => {

    const navigate = useNavigate();
    const [appointment, setAppointment] = useState();
    const [dueDateA, setDueDateA] = useState()
    const [appointerA, setappointerA] = useState()
    const [appointeeA, setappointeeA] = useState()
    const [descriptionA, setdescriptionA] = useState()
    const [titleA, setTitleA] = useState()
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
        setDueDateA(json_data.dueDate);
        setappointeeA(json_data.appointee);
        setappointerA(json_data.appointer);
        setdescriptionA(json_data.description);
        setTitleA(json_data.title);
        console.log(json_data.dueDate)
        //console.log(`${appointment.dueDate}`);
    };

    // const cancelAppointment = async () => {
    //     const res = await fetch(`${API_ROOT}/Appointments/${appointment.appointmentId}`, {
    //         method: 'DELETE',
    //         mode: 'cors'
    //     });

    //     if (res.ok) {
    //         navigate("/me");
    //         return;
    //     }
    // }
    const saveChangesAppointment = async ()=>{
        const obj ={dueDateA,"title":`${appointment.title}`, descriptionA, "type": "string", "isExpired": false}
        // console.log(obj);
        // navigate(`/appointment/${id}/update`)
        // const res = await fetch(`${API_ROOT}/Appointments/${appointment.appointmentId}`, {
        //             method: 'PUT',
        //             mode: 'cors',
        //             headers: {
        //                 'Access-Control-Allow-Origin': '*',
        //                 'Content-Type': 'application/json',
        //                 'Accept': 'application/json'},
        //             body:JSON.stringify(obj),
                    
        //         });
        
        //         if (res.ok) {
        //             navigate("/me");
        //             return;
        //         }
        axios.put(`${API_ROOT}/Appointments/${appointment.appointmentId}`, obj)
        .then( navigate("/me"))
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
                            <h1><input type="text" required value={titleA} onChange={ (e) =>{ setTitleA(e.target.value)}}></input></h1>
                        </Container>

                        <Container style={{ width: '100%', padding: '1rem', backgroundColor: "#8fc3e3" }}>
                            <form>
                                <h3>Created on:<input type="text" required value={dueDateA} onChange={ (e) =>{ setDueDateA(e.target.value)}}></input></h3>
                                
                                <h3>Created by: <input type="text" required value={appointerA} onChange={ (e) =>{ setappointerA(e.target.value)}}></input></h3>
                               
                                <h3>Appointed to: <input type="text" required value={appointeeA} onChange={ (e) =>{ setappointeeA(e.target.value)}}></input></h3>

                                <h3>Description: <input type="text" required value={descriptionA} onChange={ (e) =>{ setdescriptionA(e.target.value)}}></input></h3>

                            </form>
                            <Button variant="contained" style={{ margin: '0 auto 0 1rem', border: '2px solid', color: 'green' }} onClick={saveChangesAppointment}>Save</Button>
                        </Container>
                    </>
                }
            </div>
        )
}

export default AppointmentUpdateDataContainer;