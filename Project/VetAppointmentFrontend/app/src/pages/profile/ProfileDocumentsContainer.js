import Container from '@mui/material/Container';
import ToggleButton from '@mui/material/ToggleButton';
import ToggleButtonGroup from '@mui/material/ToggleButtonGroup';
import * as React from 'react';
import BaseDataTable from './BaseDataTable';

function createAppointmentRowEntry(id, title, dueDate, dueTime, appointer) {
    return {
        id, 
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

function createMedicalHistoryRowEntry(id, title, date, appointer) {
    return {
        id,
        title,
        date,
        appointer,
        button: {
            isButton: true,
            text: 'View'
        }
    };
}

const ProfileDocumentsContainer = () => {
    const [option, setOption] = React.useState('appointments');

    const handleChange = (event, newOption) => {
        setOption(newOption);
    };

    const appointmentsTableHeaderValues = [
        {
            id: 'title',
            numeric: false,
            disablePadding: true,
            canBeSorted: false,
            label: 'Title',
        },
        {
            id: 'dueDate',
            numeric: true,
            disablePadding: false,
            canBeSorted: true,
            label: 'Due Date',
        },
        {
            id: 'dueTime',
            numeric: true,
            disablePadding: false,
            canBeSorted: true,
            label: 'Due Time',
        },
        {
            id: 'appointer',
            numeric: true,
            disablePadding: false,
            canBeSorted: false,
            label: 'Appointer',
        },
        {
            id: 'view',
            numeric: true,
            disablePadding: false,
            canBeSorted: false,
            label: 'View',
        }
    ]

    const medicalHistoryHeaderValues = [
        {
            id: 'title',
            numeric: false,
            disablePadding: true,
            canBeSorted: false,
            label: 'Title',
        },
        {
            id: 'date',
            numeric: true,
            disablePadding: false,
            canBeSorted: true,
            label: 'Date',
        },
        {
            id: 'appointer',
            numeric: true,
            disablePadding: false,
            canBeSorted: false,
            label: 'Appointer',
        },
        {
            id: 'view',
            numeric: true,
            disablePadding: false,
            canBeSorted: false,
            label: 'View',
        }
    ]

    const appointmentsData = [
        createAppointmentRowEntry(1, 'Appt. 1', '30-10-2022', '16:00', 'Dr. Smith'),
        createAppointmentRowEntry(2, 'Appt. 2', '30-11-2022', '12:00', 'Dr. Smith'),
        createAppointmentRowEntry(3, 'Appt. 3', '30-12-2022', '13:30', 'Dr. Johnson'),
        createAppointmentRowEntry(4, 'Appt. 4', '10-01-2023', '14:20', 'Dr. Smith'),
        createAppointmentRowEntry(5, 'Appt. 5', '15-02-2023', '15:10', 'Dr. Johnson'),
        createAppointmentRowEntry(6, 'Appt. 6', '30-03-2023', '12:10', 'Dr. Smith')
    ]

    const medicalHistoryData = [
        createMedicalHistoryRowEntry(1, 'MH. 1', '30-10-2021', 'Dr. Smith'),
        createMedicalHistoryRowEntry(2, 'MH. 2', '30-11-2021', 'Dr. Smith'),
        createMedicalHistoryRowEntry(3, 'MH. 3', '30-12-2020', 'Dr. Johnson'),
        createMedicalHistoryRowEntry(4, 'MH. 4', '10-01-2020', 'Dr. Smith'),
        createMedicalHistoryRowEntry(5, 'MH. 5', '15-02-2022', 'Dr. Johnson'),
        createMedicalHistoryRowEntry(6, 'MH. 6', '30-03-2022', 'Dr. Smith')
    ]

    return (
        <>
            <Container style={{ width: '100%', height: '5rem', padding: '1rem', backgroundColor: "#8fc3e3" }}>
                <ToggleButtonGroup
                    color="primary"
                    value={option}
                    exclusive
                    onChange={handleChange}
                    aria-label="Category"
                >
                    <ToggleButton color="secondary" value="appointments">Appointments</ToggleButton>
                    <ToggleButton color="secondary" value="medical_history">Medical History</ToggleButton>
                </ToggleButtonGroup>
            </Container>
            <Container style={{ width: '100%', padding: '1rem', backgroundColor: "#8fc3e3" }}>
                {option === 'appointments' && <BaseDataTable tableHeaderValues={appointmentsTableHeaderValues} tableRows={appointmentsData} />}
                {option === 'medical_history' && <BaseDataTable tableHeaderValues={medicalHistoryHeaderValues} tableRows={medicalHistoryData} />}
            </Container>
        </>
    )
}

export default ProfileDocumentsContainer;