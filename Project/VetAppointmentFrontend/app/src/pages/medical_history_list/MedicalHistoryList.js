import Container from '@mui/material/Container';
import * as React from 'react';
import BaseDataTable from '../../components/data_table/BaseDataTable';


const createMedicalHistoryRowEntry = (title, appointmentId, prescriptionId) => {
    return {
        title,
        appointment: {
            isButton: true,
            href: `/appointment/${appointmentId}`,
            text: 'Appointment'
        },
        prescription: {
            isButton: true,
            href: `/prescriptions`,
            text: 'Prescription'
        }
    };
}

const MedicalHistoryList = ({ medicalHistoryEntries }) => {
    const medicalHistoryHeaderValues = [
        {
            id: 'title',
            numeric: false,
            disablePadding: false,
            canBeSorted: false,
            label: 'Title',
        }
    ]

    const [medicalHistoryData, setMedicalHistoryData] = React.useState([]);

    React.useEffect(() => {
        if (typeof medicalHistoryEntries === 'undefined') return;
        setMedicalHistoryData([...medicalHistoryEntries.map((item) => {
            return createMedicalHistoryRowEntry(item.title, item.appointmentId, item.prescriptionId);
        })])
    }, [medicalHistoryEntries])

    return (
        <>
            <Container style={{ width: '100%', padding: '1rem', backgroundImage: 'url(/img/dog_paws_pattern.jpg)', backgroundSize: '150%' }}>
                <BaseDataTable tableHeaderValues={medicalHistoryHeaderValues} tableRows={medicalHistoryData} />
            </Container>
        </>
    )
}

export default MedicalHistoryList;