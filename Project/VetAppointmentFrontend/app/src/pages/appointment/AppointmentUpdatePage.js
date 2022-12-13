import * as React from 'react';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import Grid from '@mui/material/Grid';
import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Paper from '@mui/material/Paper';
import { useEffect } from 'react';
import AppointmentUpdateDataContainer from './AppointmentUpdateDataContainer';
import { Button } from '@mui/material';
import Container from '@mui/material/Container';

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
const AppointmentUpdatePage = () => {
    const [data, setData] = React.useState({ hits: [] });
    useEffect(() => { document.body.style.backgroundColor = '#ebf6fc' }, [])
    return (
        <>
        <ThemeProvider theme={profileTheme}>
            <Box sx={{ flexGrow: 1, margin: '2rem auto 0 auto' }}>
                <Grid container spacing={2}>
                    <Grid item xs={1} />
                    <Grid item xs={8}>
                        <Item>
                            <AppointmentUpdateDataContainer />
                        </Item>
                    </Grid>
                    <Grid item xs={2}>
                        <Item>
                            <Container style={{ width: '100%', height: 'fit-content', padding: '1rem', backgroundColor: "#8fc3e3" }}>
                                <Button variant="contained" style={{ margin: '0 auto', border: '2px solid', color: 'white' }} href='/me'>Back</Button>
                            </Container>
                        </Item>
                    </Grid>
                    <Grid item xs={1} />
                </Grid>
            </Box>
        </ThemeProvider>
    </>
    )
}

export default AppointmentUpdatePage;