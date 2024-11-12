import { AppBar, Box, Button, Toolbar, Typography } from '@mui/material'
import { useState } from 'react';
import Cart from './Cart';

export default function Header()
{
  const [open, setOpen] = useState(false);

  const toggleDrawer = () => {
    setOpen(!open);
  }

   return(
    <Box sx={{ flexGrow: 1, backgroundColor: 'f9f9f9' }}>
      <AppBar position="static" sx={{backgroundColor: '#f9f9f9', color: '#1b1b1b'}}>
        <Toolbar>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
           Sana Commerce 
          </Typography>
          <Button onClick={toggleDrawer} color="inherit">Cart</Button>
          <Cart open={open} toggleDrawer={toggleDrawer} />
        </Toolbar>
      </AppBar>
    </Box>
   ) 
}