import Container from '@mui/material/Container';
import ToggleButton from '@mui/material/ToggleButton';
import ToggleButtonGroup from '@mui/material/ToggleButtonGroup';
import * as React from 'react';
import BaseDataTable from './BaseDataTable';

function createAppointmentRowEntry(appointmentId, title, dueDate, dueTime, appointer) {
    return {
        title,
        dueDate,
        dueTime,
        appointer,
        button: {
            isButton: true,
            href: `/appointment/${appointmentId}`,
            text: 'View'
        }
    };
}

function createMedicalHistoryRowEntry(medicalEntryId, title, date, appointer) {
    return {
        title,
        date,
        appointer,
        button: {
            isButton: true,
            href: `/medical=entry/${medicalEntryId}`,
            text: 'View'
        }
    };
}

const ProfileDocumentsContainer = ({ appointments, medicalEntries }) => {
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

    const [appointmentsData, setAppointmentsData] = React.useState([])
    const [medicalHistoryData, setMedicalHistoryData] = React.useState([])

    React.useEffect(() => {
        if (typeof appointments === 'undefined') return;
        setAppointmentsData([...appointments.map((item) => {
            return createAppointmentRowEntry(item.appointmentId, item.title, item.dueDate, item.dueTime, item.appointer);
        })])
    }, [appointments])

    React.useEffect(() => {
        if (typeof medicalEntries === 'undefined') return;
        setMedicalHistoryData([...medicalEntries.map((item) => {
            return createMedicalHistoryRowEntry(item.medicalHistoryEntryId, item.title, item.date, item.appointer);
        })])
    }, [appointments])

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