import { zodResolver } from '@hookform/resolvers/zod';
import { FormControl, IconButton, InputLabel, MenuItem, Select, Typography } from '@mui/material';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import TextField from '@mui/material/TextField';
import { Box } from '@mui/system';
import * as React from 'react';
import { useForm } from 'react-hook-form';
import CloseIcon from '@mui/icons-material/Close';
import { API_ROOT } from '../../env';
import { object, preprocess, string, number } from 'zod';


const CreateAppointmentSchema = object({
    title: string().min(1, 'Title is required'),
    date: string(),
    appointerId: string().min(1,'Appointer is required'),
    appointeeId: string().min(1,'Appointee required'),
    description: string()
});

const CreateAppointmentModal = ({currentUserId,currentUserName, isOpen, handleClose, updateAppointmentList, usersList }) => {
    const [open, setOpen] = React.useState(false);
    const [user, setUser] = React.useState('');
    const [date, setDate] = React.useState('');

    const {
        register,
        handleSubmit,
        reset,
        formState: { errors }
    } = useForm({
        resolver: zodResolver(CreateAppointmentSchema)
    });

    React.useEffect(() => {
        if (typeof isOpen === 'undefined')
            return;
        if (isOpen) {
            reset();
        }
        setOpen(isOpen);
    }, [isOpen])


    const submitAppointmentUpdate = async (data) => {
        const res = await fetch(`${API_ROOT}/v1.0/Appointments/`, {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                appointerId: currentUserId,
                appointeeId: data.appointeeId,
                dueDate: data.date,
                title: data.title,
                description: data.description,
                type: "string"
            })
        });

        window.location.reload();
        if (res.ok) {
            let jsonData = await res.json();
            updateAppointmentList(jsonData);
            handleClose();
            return;
        }
    }

    const processSubmit = (data) => {
        submitAppointmentUpdate(data);
        console.log(data)
    };

    const handleUserChange = (event) => {
        setUser(event.target.value);
    };
    const handleDateChange = (event) => {
        setDate(event.target.value);
    };


    return (
        <>
            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>
                    <span style={{ fontWeight: '600', marginRight: '2rem' }}>Create a new Appointment</span>
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
                   {usersList.length > 1 ?
                        <Box component="form" noValidate onSubmit={handleSubmit(processSubmit)} sx={{ mt: 1 }}>
                            <TextField
                                {...register("title", { required: true })}
                                margin="title"
                                required
                                fullWidth
                                id="title"
                                label="Title"
                                name="title"
                                type="string"
                                autoFocus
                                error={!!errors['title']}
                                helperText={errors['title'] ? errors['title'].message : ''}
                                style={{ margin: '1rem 0' }}
                            />
                            <TextField 
                             {...register("appointerId", { required: true })}
                                InputProps={{
                                    readOnly: true,
                                }}
                                margin="appointerId"
                                required
                                fullWidth
                                id="outlined-disabled"
                                label="Appointer"
                                name="title"
                                type="string"
                                autoFocus
                                error={!!errors['appointerId']}
                                helperText={errors['appointerId'] ? errors['appointerId'].message : ''}
                                style={{ margin: '1rem 0' }}
                                defaultValue={currentUserName}
                               
                            /> 
                            <FormControl fullWidth style={{ margin: '1rem 0' }}>
                                <InputLabel id="selectAppointee">Appointee</InputLabel>
                                <Select
                                    {...register("appointeeId", { required: true })}
                                    labelId="selectAppointeeIdLabel"
                                    id="selectAppointeeId"
                                   // value={user}
                                    label="Appointee"
                                    onChange={handleUserChange}
                                >
                                    {usersList.map(user => {
                                        return (
                                            <MenuItem value={user.userId}>{user.username}</MenuItem>
                                        )
                                    })}
                                </Select>
                            </FormControl> 
                             <TextField
                                {...register("date", { required: true })}
                                    id="datetime-local"
                                    label="Next appointment"
                                    type="datetime-local"
                                    defaultValue="2023-01-10T10:30"
                                    fullWidth
                                    sx={{ width: 250 }}
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                    
                            />
                            <br></br>
                            <TextField
                                {...register("description", {required: false})}
                                    id="description"
                                    labelId="description"
                                    fullWidth
                                    multiline
                                    rows={5}
                                    defaultValue="Write a Description"
                                />
                            <Button
                                type="submit"
                                fullWidth
                                variant="contained"
                                sx={{ mt: 3, mb: 2 }}
                            >
                                Create
                            </Button>
                        </Box> :
                        <Box>
                            <Typography variant="h5" component="div" style={{ overflow: 'hidden', padding: '0.5rem' }} >
                                No users to appoint
                            </Typography>
                            <Typography variant="h7" component="div" style={{ overflow: 'hidden', padding: '0.5rem' }} >
                                Add uses to be able to create an appointment
                            </Typography>
                        </Box>} 
                </DialogContent>
            </Dialog>
        </>
    );
}

export default CreateAppointmentModal;