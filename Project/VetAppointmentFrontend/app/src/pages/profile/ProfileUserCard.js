import Box from '@mui/material/Box';
import Button from "@mui/material/Button";
import Card from '@mui/material/Card';
import CardActions from "@mui/material/CardActions";
import CardContent from '@mui/material/CardContent';
import Typography from "@mui/material/Typography";
import Modal from '@mui/material/Modal';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import * as React from 'react';
import { disconnectUser, getAccessToken, getRefreshToken, makeRequestWithJWT } from '../../util/JWTUtil';
import EditOfficeModal from './EditOfficeModal';
import CreateOfficeModal from './CreateOfficeModal';
import { API_ROOT, BACKGROUND_COLOR } from '../../env';
import { DialogContentText, FormControl, IconButton, InputLabel, MenuItem, Select } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import { useLocation, useNavigate } from "react-router-dom";

const ProfileUserCard = ({ username, isMedic, joinedDate, hasOffice, officeId }) => {
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const [isEditOfficeModalOpen, setIsEditOfficeModalOpen] = React.useState(false);
  const [isCreateOfficeModalOpen, setIsCreateOfficeModalOpen] = React.useState(false);
  const [currentModalOffice, setCurrentModalOffice] = React.useState({});
  const [address, setAddress]= React.useState('');
  const [isAllowed, setIsAllowed] = React.useState(false);

  const navigate=useNavigate();

  const openOfficeModal = (officeId, address) => {
    setCurrentModalOffice({
      officeId,
      address
    });
    setIsEditOfficeModalOpen(true);
  }

  const handleEditOfficeModalClose = () => {
    setIsEditOfficeModalOpen(false);
  }

  const openCreateOfficeModal=()=>{
    setIsCreateOfficeModalOpen(true);
  }
  const handleCreateOfficeModal=()=>{
    setIsCreateOfficeModalOpen(false);
  }

  const fetchAddressData = async () => {
    let res = await makeRequestWithJWT(
      `${API_ROOT}/v1.0/Offices/${officeId}`, {
      method: 'GET',
      mode: 'cors'
    }, {
      accessToken: getAccessToken(),
      refreshToken: getRefreshToken()
    }
    )
    if (res.status === 401) {
      disconnectUser();
      navigate('/login');
      return;
    }

    if (res.status === 403) {
      setIsAllowed(false);
      return;
    }

    const officeData = await res.json();
    setAddress(officeData.address);
  }
  
  const deleteOffice = async () => {
    let res = await makeRequestWithJWT(
      `${API_ROOT}/v1.0/Offices/${officeId}`, {
      method: 'DELETE',
      mode: 'cors'
    }, {
      accessToken: getAccessToken(),
      refreshToken: getRefreshToken()
    }
    )
    if (res.status === 401) {
      disconnectUser();
      navigate('/login');
      return;
    }

    if (res.status === 403) {
      setIsAllowed(false);
      return;
    }
  }
  let officeButton = isMedic ?
    <CardActions>
      <Button
        type="button"
        style={{ margin: '0 auto', width: '14rem', backgroundColor: "#ffffff", color: '#186bc9', fontWeight: '600' }}
        variant="contained"
        sx={{ mt: 3, mb: 2 }}
        onClick={handleOpen}
      >My office
      </Button>
    </CardActions> : <></>
    if(hasOffice===true)
    {
      fetchAddressData();
      return (
        <Box sx={{ width: '100%', height: '100%' }}>
          <Card variant="outlined" style={{ backgroundImage: 'url(/img/dog_paws_pattern.jpg)', backgroundSize: '150%' }}>
            <CardContent style={{ color: 'white', textAlign: 'center', padding: '1rem' }}>
              <Typography variant="h4" component="div" style={{ overflow: 'hidden', paddingLeft: '0.5rem', paddingRight: '0.5rem', fontWeight: '600', textShadow: '2px 2px 4px black' }} >
                {username}
              </Typography>
              <Typography variant="h6" style={{ margin: '1rem auto 0.5rem auto', textShadow: '2px 2px 4px black', fontWeight: '600' }}>
                {isMedic ? 'Medic account' : 'Client account'}
              </Typography>
              <Typography variant="h6" style={{ margin: '1rem auto 0.5rem auto', textShadow: '2px 2px 4px black', fontWeight: '600' }}>
                Joined {joinedDate}
              </Typography>
            </CardContent>
            {officeButton}
            <Dialog
              open={open}
              onClose={handleClose}
            >
              <DialogTitle>
                <span style={{ fontWeight: '600', marginRight: '2rem' }}>Address</span>
                <IconButton
                  aria-label="close"
                  onClick={handleClose}
                  sx={{
                    position: 'absolute',
                    right: 8,
                    top: 8,
                    color: (theme) => theme.palette.grey[500],
                  }}
                >
                  <CloseIcon />
                </IconButton>
              </DialogTitle>
              <DialogContent>
                <DialogContentText>{address}</DialogContentText>
              </DialogContent>
              <DialogActions>
              <Button onClick={openOfficeModal}>Edit</Button>
              <EditOfficeModal
                isOpen={isEditOfficeModalOpen}
                handleClose={handleEditOfficeModalClose}
                officeId={officeId}
              />
              <Button onClick={deleteOffice}>Delete</Button>
            </DialogActions>
            </Dialog>
          </Card>
        </Box>
      )
    }
    else
    {
      return (
        <Box sx={{ width: '100%', height: '100%' }}>
          <Card variant="outlined" style={{ backgroundImage: 'url(/img/dog_paws_pattern.jpg)', backgroundSize: '150%' }}>
            <CardContent style={{ color: 'white', textAlign: 'center', padding: '1rem' }}>
              <Typography variant="h4" component="div" style={{ overflow: 'hidden', paddingLeft: '0.5rem', paddingRight: '0.5rem', fontWeight: '600', textShadow: '2px 2px 4px black' }} >
                {username}
              </Typography>
              <Typography variant="h6" style={{ margin: '1rem auto 0.5rem auto', textShadow: '2px 2px 4px black', fontWeight: '600' }}>
                {isMedic ? 'Medic account' : 'Client account'}
              </Typography>
              <Typography variant="h6" style={{ margin: '1rem auto 0.5rem auto', textShadow: '2px 2px 4px black', fontWeight: '600' }}>
                Joined {joinedDate}
              </Typography>
            </CardContent>
            {officeButton}
            <Dialog
              open={open}
              onClose={handleClose}
            >
              <DialogTitle>
                <span style={{ fontWeight: '600', marginRight: '2rem' }}>Address</span>
                <IconButton
                  aria-label="close"
                  onClick={handleClose}
                  sx={{
                    position: 'absolute',
                    right: 8,
                    top: 8,
                    color: (theme) => theme.palette.grey[500],
                  }}
                >
                  <CloseIcon />
                </IconButton>
              </DialogTitle>
              <DialogContent>
                <DialogContentText>{address}</DialogContentText>
              </DialogContent>
              <DialogActions>
              <Button onClick={openCreateOfficeModal}>Create</Button>
              <CreateOfficeModal
                isOpen={isCreateOfficeModalOpen}
                handleClose={handleCreateOfficeModal}
              />
            </DialogActions>
            </Dialog>
          </Card>
        </Box>
      )
    }
}

export default ProfileUserCard;