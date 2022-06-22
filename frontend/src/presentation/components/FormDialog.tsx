import Button from '@mui/material/Button/Button';
import Dialog from '@mui/material/Dialog/Dialog';
import DialogActions from '@mui/material/DialogActions/DialogActions';
import DialogContent from '@mui/material/DialogContent/DialogContent';
import DialogTitle from '@mui/material/DialogTitle/DialogTitle';
import React from 'react';

export interface FormDialogProps {
	title: string;
	children: React.ReactNode;
}

const FormDialog: React.FC<FormDialogProps> = ({ title, children }) => {
	const [open, setOpen] = React.useState(false);

	const handleOpen = () => {
		setOpen(true);
	};

	const handleClose = () => {
		setOpen(false);
	};

	return (
		<Dialog open={open} onClose={handleClose}>
			<DialogTitle>{title}</DialogTitle>
			<DialogContent>{children}</DialogContent>
			<DialogActions>
				<Button onClick={handleClose}>Cancel</Button>
				<Button onClick={handleClose}>Subscribe</Button>
			</DialogActions>
		</Dialog>
	);
};

export default FormDialog;
