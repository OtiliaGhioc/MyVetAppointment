import Box from '@mui/material/Box';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from "@mui/material/Typography";
import CardActions from "@mui/material/CardActions";
import Button from "@mui/material/Button";

const ProfileUserCard = ({ username, isMedic, joinedDate }) => {
    let officeButton = isMedic ? 
        <CardActions>
            <Button style={{ margin: '0 auto', border: '2px solid' }} color="secondary" variant="outlined">
                My Office
            </Button>
        </CardActions> : <></>
    return (
        <Box sx={{ width: '100%', height: '100%'}}>
            <Card variant="outlined" style={{ backgroundColor: "#8fc3e3" }}>
                <CardContent style={{ color: 'white', textAlign: 'center', padding: '1rem' }}>
                    <Typography variant="h5" component="div" style={{ overflow: 'hidden', paddingLeft: '0.5rem', paddingRight: '0.5rem' }}>
                        {username}
                    </Typography>
                    <Typography variant="body2" style={{ margin: '1rem auto 0 auto' }}>
                        I am a: {isMedic ? 'Medic' : 'Client'}
                    </Typography>
                    <Typography variant="body2" style={{ margin: '1rem auto 0 auto' }}>
                        Joined {joinedDate}
                    </Typography>
                </CardContent>
                {officeButton}
            </Card>
        </Box>
    )
}

export default ProfileUserCard;