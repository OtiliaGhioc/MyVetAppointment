import Container from '@mui/material/Container';
import * as React from 'react';
import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from 'react';
import { API_ROOT } from '../../env';
import { Button } from '@mui/material';
import axios from 'axios';


const AppointmentCreateDataContainer = () => {

    const navigate = useNavigate();
    const [appointment, setAppointment] = useState();
    const [dueDateA, setDueDateA] = useState()
    const [appointerA, setappointerA] = useState()
    const [appointeeA, setappointeeA] = useState()
    const [descriptionA, setdescriptionA] = useState()
    const [titleA, setTitleA] = useState()
   

    const saveAppointment = async ()=>{
        const obj ={"appointerId":"1775a144-0134-43f1-971a-bce94639707c" ,"appointeeId":"009452fc-f236-47a0-a183-a82716264db8",  dueDateA, titleA, descriptionA, "type": "string"}
        console.log(obj);
        const res = await fetch(`${API_ROOT}/Appointments`, {
                    method: 'POST',
                    mode: 'cors',
                    headers: {
                        'Access-Control-Allow-Origin': '*',
                        'Content-Type': 'application/json',
                    },
                    body:JSON.stringify(obj),
                    
                });
        
                if (res.ok) {
                    navigate("/me");
                    return;
                }
    }

    useEffect(() => {
        document.body.style.backgroundColor = '#ebf6fc';
    }, [navigate])
    
        return (
            <div>
                {
                    <>
                        <Container style={{ width: '100%', height: '5rem', padding: '1rem', backgroundColor: "#8fc3e3" }}>
                            <h1>Enter a title  <input type="text" required  onChange={ (e) =>{ setTitleA(e.target.value)}}></input></h1>
                        </Container>

                        <Container style={{ width: '100%', padding: '1rem', backgroundColor: "#8fc3e3" }}>
                            <form>
                                <h3>Created on:<input type="text" required  onChange={ (e) =>{ setDueDateA(e.target.value)}}></input></h3>
                                
                                <h3>Created by: <input type="text" required  onChange={ (e) =>{ setappointerA(e.target.value)}}></input></h3>
                               
                                <h3>Appointed to: <input type="text" required  onChange={ (e) =>{ setappointeeA(e.target.value)}}></input></h3>

                                <h3>Description: <input type="text" required  onChange={ (e) =>{ setdescriptionA(e.target.value)}}></input></h3>

                            </form>
                            <Button variant="contained" style={{ margin: '0 auto 0 1rem', border: '2px solid', color: 'green' }} onClick={saveAppointment}>Save</Button>
                        </Container>
                    </>
                }
            </div>
        )
}

export default AppointmentCreateDataContainer;