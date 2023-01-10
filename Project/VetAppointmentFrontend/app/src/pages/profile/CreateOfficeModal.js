import { zodResolver } from '@hookform/resolvers/zod';
import { IconButton } from '@mui/material';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import TextField from '@mui/material/TextField';
import { Box } from '@mui/system';
import * as React from 'react';
import { useForm } from 'react-hook-form';
import { string, object } from 'zod';
import CloseIcon from '@mui/icons-material/Close';
import { API_ROOT } from '../../env';
import { disconnectUser, getAccessToken, getRefreshToken, makeRequestWithJWT } from '../../util/JWTUtil';
import { useLocation, useNavigate } from "react-router-dom";


const editAddressSchema = object({
    address: string()
        .min(1, 'Address is required')
        .regex(new RegExp(/[a-zA-Z\s]+ (\d{1,})(\,)? [A-Z]{2}(\,)? [0-9]{6}/i), "Should have format: Strada Zorilor 13, IS, 123456")
});

const CreateOfficeModal = ({ isOpen, handleClose }) => {
    const [open, setOpen] = React.useState(false);
    const [isAllowed, setIsAllowed] = React.useState(false);

  const navigate=useNavigate();

    const {
        register,
        handleSubmit,
        reset,
        formState: { errors }
    } = useForm({
        resolver: zodResolver(editAddressSchema)
    });

    React.useEffect(() => {
        if (typeof isOpen === 'undefined')
            return;
        if (isOpen)
            reset();
        setOpen(isOpen);
    }, [isOpen])
    
    const submitAddressUpdate = async (data) => {

        const res = await makeRequestWithJWT(
            `${API_ROOT}/v1.0/Offices/`, {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                address: data.address
            })
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

        if (res.ok) {
            // updateOfficeState(officeId, data.supplyQuantity, data.price);
             let jsonData = await res.json();
             console.log(jsonData);
            // updateOffice(jsonData);
            handleClose();
            return;
        }
    }

    const processSubmit = (data) => {
        submitAddressUpdate(data);
    };

    return (
        <>
            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>
                    <span style={{ fontWeight: '600', marginRight: '2rem' }}>Edit address</span>
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
                    <Box component="form" noValidate onSubmit={handleSubmit(processSubmit)} sx={{ mt: 1 }}>
                        <TextField
                            {...register("address", { required: true })}
                            margin="normal"
                            required
                            fullWidth
                            id="address"
                            label="Address"
                            name="address"
                            type="string"
                            autoFocus
                            error={!!errors['address']}
                            helperText={errors['address'] ? errors['address'].message : ''}
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            Submit
                        </Button>
                    </Box>
                </DialogContent>
            </Dialog>
        </>
    );
}
export default CreateOfficeModal;