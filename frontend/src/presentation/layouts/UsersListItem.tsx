import React, { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import ListItemButton from '@mui/material/ListItemButton/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon/ListItemIcon';
import PersonIcon from '@mui/icons-material/Person';
import ListItemText from '@mui/material/ListItemText/ListItemText';
import { ExpandLess, ExpandMore } from '@mui/icons-material';
import { Collapse } from '@mui/material';
import List from '@mui/material/List/List';
import PersonAddIcon from '@mui/icons-material/PersonAdd';

const UsersListItem: React.FC<{ isSuperAdmin: boolean }> = ({
	isSuperAdmin,
}) => {
	const [open, setOpen] = useState(true);
	const navigate = useNavigate();
	const { t } = useTranslation();
	const location = useLocation();

	const isUsersCreatePage = location.pathname === '/users/create';
	const isUsersPage = location.pathname === '/users';

	return (
		(isSuperAdmin && (
			<>
				<ListItemButton onClick={() => setOpen(!open)}>
					<ListItemIcon>
						<PersonIcon />
					</ListItemIcon>
					<ListItemText primary={t('homeLayout.users')} />
					{open ? <ExpandLess /> : <ExpandMore />}
				</ListItemButton>
				<Collapse in={open} timeout='auto' unmountOnExit>
					<List component='div' disablePadding>
						<ListItemButton
							sx={{ pl: 4 }}
							selected={isUsersPage}
							onClick={() => navigate('/users')}
						>
							<ListItemIcon>
								<PersonIcon />
							</ListItemIcon>
							<ListItemText primary={t('homeLayout.listUsers')} />
						</ListItemButton>
						<ListItemButton
							sx={{ pl: 4 }}
							selected={isUsersCreatePage}
							onClick={() => navigate('/users/create')}
						>
							<ListItemIcon>
								<PersonAddIcon />
							</ListItemIcon>
							<ListItemText
								primary={t('homeLayout.createUser')}
							/>
						</ListItemButton>
					</List>
				</Collapse>
			</>
		)) || <></>
	);
};

export default UsersListItem;
