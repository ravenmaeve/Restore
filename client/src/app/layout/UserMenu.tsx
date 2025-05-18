import { Button, Divider, ListItemIcon, ListItemText, Menu, MenuItem } from "@mui/material";
import  { useState } from "react";
import { User } from "../models/user";
import { Logout, Person,History } from "@mui/icons-material";
import { useLogoutMutation } from "../features/account/accountApi";

type Props = {
    user: User
}

export default function UserMenu({user}:Props) {
    const [logout] = useLogoutMutation();
 const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);
  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <div>
      <Button
        onClick={handleClick}
        color='inherit'
        size='large'
        sx={{fontSize: '1.1rem'}}
      >
        {user.email}
      </Button>
      <Menu
        id="basic-menu"
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        MenuListProps={{
          'aria-labelledby': 'basic-button',
        }}
      >
        <MenuItem>
            <ListItemIcon>
                <Person/>
            </ListItemIcon>
            <ListItemText>My Profile</ListItemText>
        </MenuItem>
        <MenuItem >
             <ListItemIcon>
                <History/>
            </ListItemIcon>
            <ListItemText>My orders</ListItemText>
        </MenuItem>
        <Divider/>
        <MenuItem onClick={logout}>
         <ListItemIcon>
                <Logout/>
            </ListItemIcon>
            <ListItemText>Logout</ListItemText>
        </MenuItem>
      </Menu>
    </div>
  );
}
