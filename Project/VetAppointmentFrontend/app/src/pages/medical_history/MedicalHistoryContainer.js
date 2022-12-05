import Container from '@mui/material/Container';
import * as React from 'react';
import ToggleButton from '@mui/material/ToggleButton';

function createMedicalHistoryEntry(title, date, appointer_name, customer_name) {
    return {
        title,
        date,
        appointer_name,
        customer_name,
    };
}

const MedicalHistoryDataContainer = () => {

    const medicalHistoryData = [
        createMedicalHistoryEntry('MH. 1', '30-10-2022','Dr. Smith', 'Naruto Uzumaki')
    ]

    return (
        <>
            <Container style={{ width: '100%', height: '5rem', padding: '1rem', backgroundColor: "#8fc3e3" }}>
                <h1>{medicalHistoryData[0].title}</h1>
            </Container>

            <Container style={{ width: '100%', padding: '1rem', backgroundColor: "#8fc3e3" }}>
                <h3>Date: {medicalHistoryData[0].date}</h3>
                <h3>Appointer: {medicalHistoryData[0].appointer_name}</h3>
                <h3>Customer: {medicalHistoryData[0].customer_name}</h3> 
               
                <ToggleButton color="secondary" value="appointment" style={{marginRight: 10}}>Appointment</ToggleButton>
                <ToggleButton color="secondary" value="prescription">Prescription</ToggleButton>
                
            </Container>
           
        </>
    )
}

export default MedicalHistoryDataContainer;