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

    return (
        <>
            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>
                    <span style={{ fontWeight: '600', marginRight: '2rem' }}>Edit Appointment</span>
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
                            {...register("description", { required: false })}
                            id="description"
                            label="Description"
                            fullWidth
                            multiline
                            rows={5}
                            defaultValue={appointmentDesc}
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                            style={{ color: 'green', backgroundColor: '#12521a' }}
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