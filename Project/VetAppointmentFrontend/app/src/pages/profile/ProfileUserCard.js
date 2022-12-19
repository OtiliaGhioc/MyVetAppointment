import Box from '@mui/material/Box';
import Button from "@mui/material/Button";
import Card from '@mui/material/Card';
import CardActions from "@mui/material/CardActions";
import CardContent from '@mui/material/CardContent';
import Typography from "@mui/material/Typography";

const ProfileUserCard = ({ username, isMedic, joinedDate, hasOffice }) => {
    let officeButton = isMedic ?
        <CardActions>
            <Button style={{ margin: '0 auto', color: '#186bc9' }} color="secondary" variant="contained">
                My Office
            </Button>
        </CardActions> : <></>
    return (
        <Box sx={{ width: '100%', height: '100%' }}>
            <Card variant="outlined" style={{ backgroundImage: 'url(/img/dog_paws_pattern.jpg)', backgroundSize: '150%' }}>
                <CardContent style={{ color: 'white', textAlign: 'center', padding: '1rem' }}>
                    <Typography variant="h5" component="div" style={{ overflow: 'hidden', paddingLeft: '0.5rem', paddingRight: '0.5rem', fontWeight: '600', textShadow: '2px 2px 4px black' }} >
                        {username}
                    </Typography>
                    <Typography variant="body2" style={{ margin: '1rem auto 0 auto', textShadow: '1px 1px 2px black' }}>
                        {isMedic ? 'Medic account' : 'Client account'}
                    </Typography>
                    <Typography variant="body2" style={{ margin: '1rem auto 0 auto', textShadow: '1px 1px 2px black' }}>
                        Joined {joinedDate}
                    </Typography>
                </CardContent>
                {officeButton}
            </Card>
        </Box>
    )
}

export default ProfileUserCard;