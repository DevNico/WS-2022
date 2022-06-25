import ListItem, { ListItemProps } from '@mui/material/ListItem/ListItem';
import ListItemIcon from '@mui/material/ListItemIcon/ListItemIcon';
import ListItemText from '@mui/material/ListItemText/ListItemText';
import React from 'react';
import { Link, useLocation } from 'react-router-dom';

interface ListItemLinkProps extends ListItemProps {
	to: { $: string };
	text: string;
	icon?: React.ReactElement;
}

const ListItemLink: React.FC<ListItemLinkProps> = ({
	to,
	text,
	icon,
	...other
}) => {
	const location = useLocation();

	return (
		<ListItem
			button
			component={Link as any}
			to={`/${to.$}`}
			selected={location.pathname.slice(1) === to.$}
			{...other}
		>
			{icon && <ListItemIcon>{icon}</ListItemIcon>}
			<ListItemText primary={text} />
		</ListItem>
	);
};

export default ListItemLink;
