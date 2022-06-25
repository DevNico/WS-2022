import React from 'react';
import {
	CorporateFare,
	ExpandLess,
	ExpandMore,
	Person,
} from '@mui/icons-material';
import { useLocation, useNavigate } from 'react-router-dom';
import ListItemButton from '@mui/material/ListItemButton/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon/ListItemIcon';
import ListItemText from '@mui/material/ListItemText/ListItemText';
import List from '@mui/material/List/List';
import Collapse from '@mui/material/Collapse/Collapse';

interface DrawerOrganisationItemProps {
	organisationName: string;
	organisationRouteName: string;
}

const DrawerOrganisationItem: React.FC<DrawerOrganisationItemProps> = ({
	organisationName,
	organisationRouteName,
}) => {
	const [open, setOpen] = React.useState(false);
	const navigate = useNavigate();
	const location = useLocation();

	const route = `/organisation/${organisationRouteName}`;
	const roles = `${route}/role`;
	const users = `${route}/user`;

	return (
		<>
			<ListItemButton onClick={() => setOpen(!open)}>
				<ListItemIcon>
					<CorporateFare />
				</ListItemIcon>
				<ListItemText primary={organisationName} />
				{open ? <ExpandLess /> : <ExpandMore />}
			</ListItemButton>
			<Collapse in={open} timeout='auto' unmountOnExit>
				<List component='div' disablePadding>
					<ListItemButton
						sx={{ pl: 4 }}
						onClick={() => navigate(users)}
						selected={location.pathname === users}
					>
						<ListItemIcon>
							<Person />
						</ListItemIcon>
						<ListItemText primary='Users' />
					</ListItemButton>
					<ListItemButton
						sx={{ pl: 4 }}
						onClick={() => navigate(roles)}
						selected={location.pathname === roles}
					>
						<ListItemIcon>
							<Person />
						</ListItemIcon>
						<ListItemText primary='Roles' />
					</ListItemButton>
				</List>
			</Collapse>
		</>
	);
};

export default DrawerOrganisationItem;
