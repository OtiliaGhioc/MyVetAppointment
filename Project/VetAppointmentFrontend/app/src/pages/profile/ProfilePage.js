import * as React from 'react';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import ProfileUserCard from './ProfileUserCard';
import Grid from '@mui/material/Grid';
import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Paper from '@mui/material/Paper';
import ProfileDocumentsContainer from './ProfileDocumentsContainer';
import { useEffect } from 'react';

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
    let username = 'Username';
    let isMedic = true;
    let joinedDate = 'March 2020';

    useEffect(() => { document.body.style.backgroundColor = '#ebf6fc' }, [])

    return (
        <ThemeProvider theme={profileTheme}>
            <Box sx={{ flexGrow: 1, margin: '2rem auto 0 auto' }}>
                <Grid container spacing={2}>
                    <Grid item xs={1} />
                    <Grid item xs={8}>
                        <Item>
                            <ProfileDocumentsContainer />
                        </Item>
                    </Grid>
                    <Grid item xs={2}>
                        <Item>
                            <ProfileUserCard username={username} isMedic={isMedic} joinedDate={joinedDate} />
                        </Item>
                    </Grid>
                    <Grid item xs={1} />
                </Grid>
            </Box>
        </ThemeProvider>
    )
}

export default ProfilePage;