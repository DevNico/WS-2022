import MuiBreadcrumbs from '@mui/material/Breadcrumbs/Breadcrumbs';
import Typography from '@mui/material/Typography/Typography';
import React from 'react';
import { useLocation } from 'react-router-dom';
import { homeRoute } from '../../Router';
import RouterLink from '../RouterLink';

const Breadcrumbs = () => {
	const location = useLocation();
	const pathnames = location.pathname.split('/').filter((x) => x);

	return (
		<MuiBreadcrumbs aria-label='breadcrumb'>
			<RouterLink underline='hover' color='inherit' to={homeRoute({})}>
				Home
			</RouterLink>
			{pathnames.map((value, index) => {
				const last = index === pathnames.length - 1;
				const to = `/${pathnames.slice(0, index + 1).join('/')}`;

				return last ? (
					<Typography color='text.primary' key={to}>
						{'breadcrumbNameMap[to]'}
					</Typography>
				) : (
					<RouterLink
						underline='hover'
						color='inherit'
						to={{ $: to }}
						key={to}
					>
						{'breadcrumbNameMap[to]'}
					</RouterLink>
				);
			})}
		</MuiBreadcrumbs>
	);
};

export default Breadcrumbs;
