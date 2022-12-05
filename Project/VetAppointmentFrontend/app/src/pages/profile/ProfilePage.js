import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import Paper from '@mui/material/Paper';
import { createTheme, styled, ThemeProvider } from '@mui/material/styles';
import * as React from 'react';
import { useEffect } from 'react';
import { useNavigate } from "react-router-dom";
import { API_ROOT } from '../../env';
import ProfileDocumentsContainer from './ProfileDocumentsContainer';
import ProfileUserCard from './ProfileUserCard';

const profileTheme = createTheme({
    palette: {
        primary: {
            main: '#8fc3e3',
        },
        secondary: {
            main: '#ffffff',
        },
    },
});

const Item = styled(Paper)(({ theme }) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    ...theme.typography.body2,
    textAlign: 'center',
    color: theme.palette.text.secondary,
}));

const ProfilePage = () => {
    const [username, setUsername] = React.useState('username');
    const [isMedic, setIsMedic] = React.useState(false);
    const [hasOffice, setHasOffice] = React.useState(false);
    const [joinedDate, setJoinedDate] = React.useState('March 2020');
    const [appointments, setAppointments] = React.useState([]);
    const [medicalEntries, setMedicalEntries] = React.useState([]);

    const navigate = useNavigate();

    const setPageData = (data) => {
        setUsername(data.username);
        setIsMedic(data.isMedic);
        setJoinedDate(data.joinedDate);
        setHasOffice(data.hasOffice);
        setAppointments(data.appointments);
    }

    useEffect(() => {
        document.body.style.backgroundColor = '#ebf6fc';

        let userId = '1775a144-0134-43f1-971a-bce94639707c'; // to be taken from jwt

        const fetchData = async () => {
            const res = await fetch(`${API_ROOT}/Users/${userId}`, {
                method: 'GET',
                mode: 'cors'
            });

            if (!res.ok) {
                navigate("/not-found");
                return;
            }

            const jsonData = await res.json();
            setPageData(jsonData);
        }
        fetchData();
    }, [navigate])

    return (
        <ThemeProvider theme={profileTheme}>
            <Box sx={{ flexGrow: 1, margin: '2rem auto 0 auto' }}>
                <Grid container spacing={2}>
                    <Grid item xs={1} />
                    <Grid item xs={8}>
                        <Item>
                            <ProfileDocumentsContainer appointments={appointments} medicalEntries={medicalEntries}/>
                        </Item>
                    </Grid>
                    <Grid item xs={2}>
                        <Item>
                            <ProfileUserCard username={username} isMedic={isMedic} joinedDate={joinedDate} hasOffice={hasOffice} />
                        </Item>
                    </Grid>
                    <Grid item xs={1} />
                </Grid>
            </Box>
        </ThemeProvider>
    )
}

export default ProfilePage;