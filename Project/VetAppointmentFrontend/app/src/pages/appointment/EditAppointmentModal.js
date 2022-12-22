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
import { number, string, object, preprocess } from 'zod';
import CloseIcon from '@mui/icons-material/Close';
import { API_ROOT } from '../../env';
import { useNavigate, useParams } from "react-router-dom";

const editAppointmentSchema = object({
    title: string().min(1, 'Title is required'),
    date: string().min(1, "Choose a date"),
    description: string()
});

const EditAppointmentModal = ({ isOpen, handleClose, appointmentId, appointmentTitle, appointmentDesc, appointmentData }) => {

    const [open, setOpen] = React.useState(false);
    const navigate = useNavigate();

    const {
        register,
        handleSubmit,
        reset,
        formState: { errors }
    } = useForm({
        resolver: zodResolver(editAppointmentSchema)
    });

    React.useEffect(() => {
        if (typeof isOpen === 'undefined')
            return;
        if (isOpen)
            reset();
        setOpen(isOpen);
    }, [isOpen])

    const submitAppointmentUpdate = async (data) => {
        const res = await fetch(`${API_ROOT}/v1.0/Appointments/${appointmentId}/`, {
            method: 'PUT',
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                dueDate: data.date,
                title: data.title,
                description: data.description,
                type: "string",
                isExpired: false
            })
        });
        if (res.ok) {
            window.location.reload();
            handleClose();
            return;
        }
    }

    const processSubmit = (data) => {
        submitAppointmentUpdate(data);
    };

    console.log(appointmentData)
    return (
        <>
            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>
                    <span style={{ fontWeight: '600', marginRight: '2rem' }}>Edit Appointment with id: {appointmentId}</span>
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
                                defaultValue={appointmentTitle}
                            />
                             <TextField
                                {...register("date", { required: true })}
                                    id="datetime-local"
                                    label="Next appointment"
                                    type="datetime-local"
                                    fullWidth
                                    defaultValue={appointmentData}
                                    sx={{ width: 250 }}
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                    
                            />
                            <br></br>
                            <label>
                                Description
                                <TextField
                                    {...register("description", {required: false})}
                                        id="description"
                                        labelId="description"
                                        fullWidth
                                        multiline
                                        rows={5}
                                        defaultValue={appointmentDesc}
                                    />
                            </label>
                            <Button
                                type="submit"
                                fullWidth
                                variant="contained"
                                sx={{ mt: 3, mb: 2 }}
                                style={{ color: 'green' }}
                            >
                                Edit
                            </Button>
                    </Box>
                </DialogContent>
            </Dialog>
        </>
    );
}

export default EditAppointmentModal;