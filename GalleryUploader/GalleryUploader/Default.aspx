﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GalleryUploader.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Gallery</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" media="screen,projection" href="css/reset.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection" href="css/main.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection" href="css/color.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection" href="css/prettyPhoto.css" />
    <link rel="stylesheet" href="css/superfish.css" type="text/css" media="all" />
    <!--[if IE]><link rel="stylesheet" type="text/css" media="screen,projection" href="css/ie6.css" /><![endif]-->
    <script src="js/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="js/cufon-yui.js" type="text/javascript"></script>
    <script src="js/cufon-replace.js" type="text/javascript"></script>
    <script src="js/Quicksand_Bold_700.font.js" type="text/javascript"></script>
    <script src="js/Quicksand_Book_400.font.js" type="text/javascript"></script>
    <script src="js/jquery.scrollTo-min.js" type="text/javascript"></script>
    <script src="js/jquery.prettyPhoto.js" type="text/javascript"></script>
    <script src="js/jquery.infinitecarousel.js" type="text/javascript"></script>
    <script src="js/superfish.js" type="text/javascript"></script>
    <script src="js/hoverIntent.js" type="text/javascript"></script>
    <script src="js/jquery.validate.pack.js" type="text/javascript"></script>
    <script src="js/script.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">

<div class="ambitios_main" id="options-examples"  > 

  <!-- Header -->

  <div class="ambitios_header_tile_left"></div>
  <div class="ambitios_header_tile_right"></div>
  <div class="ambitios_row_tile_sub"></div>
  <div class="ambitios_row_tile_sub_right"></div>
  <div class="ambitios_header">

    <div class="ambitios_head"> 

      <!-- logo --> 
      <a href="index.html" class="ambitios_logo"><img src="images/ambitios_logo.png" alt="" /></a> 
      <!-- EOF logo --> 

      <!-- menu -->
      <ul class="ambitios_menu">
        <li><a href="Default.aspx" class="ambitios_active"><span><span>Home</span></span></a></li>
        <li><a href="Upload.aspx"><span><span>Upload</span></span></a></li>
      </ul>
      <!-- EOF menu --> 

    </div>

  </div>

  <div class="ambitios_rows_sub">

    <div class="ambitios_container_16">

      <div class="ambitios_wrapper">

        <div class="ambitios_grid_8 ambitios_alpha">

          <h1 class="ambitios_uppercase">Theme features</h1>

        </div>

        <div class="ambitios_grid_8"> We are going to prepare a lot of tasty features for you. <br/>

          So stay tuned! </div>

      </div>

    </div>

  </div>

  <!-- EOF Header --> 

  <!-- Content -->

  <div class="ambitios_content">
    <div class="ambitios_container_16">

        <asp:Repeater ID="repeater" runat="server">
            <ItemTemplate>
            <div class="ambitios_wrapper ambitios_p4">
                <div class="ambitios_col3 ambitios_alpha">
                    <div class="ambitios_wrapper ambitios_p2">
                        <a href="#" class="ambitios_picture ambitios_fleft  ambitios_lightbox_image" >
                        <img src="<%# GetPath(Container.DataItem, 0) %>" alt="" width="282px" height="160px" />
                        </a>
                    </div>
                    <h3 class="ambitios_uppercase">2x Home page layouts</h3>
                </div>
                <div class="ambitios_col3">
                    <div class="ambitios_wrapper ambitios_p2">
                        <a href="#" class="ambitios_picture ambitios_fleft  ambitios_lightbox_image" >
                            <img src="<%# GetPath(Container.DataItem, 1) %>" alt="" width="282px" height="160px" />
                        </a>
                        </div>
                    <h3 class="ambitios_uppercase">Ajax Contact form</h3>
                </div>
                <div class="ambitios_col3 ambitios_omega">
                    <div class="ambitios_wrapper ambitios_p2">
                        <a href="#" class="ambitios_picture ambitios_fleft  ambitios_lightbox_image" >
                            <img src="<%# GetPath(Container.DataItem, 2) %>" alt="" width="282px" height="160px" />
                        </a>
                    </div>
                    <h3 class="ambitios_uppercase">3x portfolio layouts.</h3>
                </div>

            </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
  </div>

  <!-- EOF Content --> 

  <!-- Row top -->

  <div class="ambitios_row_top">

    <div class="ambitios_container_16" id="toc"> <a href="#top" id="top-link">to top</a> </div>

  </div>

  <!-- EOF Row top --> 

<!-- Row second -->
  <div class="ambitios_row2_bg_g">
    <div class="ambitios_row2_bg_g_left"></div>
    <div class="ambitios_row2_bg_g_right"></div>
    <div class="ambitios_row2_bg">
      <div class="ambitios_container_16">
        <div class="ambitios_wrapper">
          <div class="ambitios_left"> 
            <!-- footer_widget -->
            <div class="ambitios_footer_widget"> Additional footer advert area </div>
            <!-- EOF footer_widget --> 
          </div>
          <div class="ambitios_right"> 
            <!-- footer_widge text -->
            <div class="ambitios_text"> This is something your client can't miss. You can add text images, tweets or news here. But of course you can hide this area, If you want, and make footer smaller. Lorem ipsum dolor sit amet, consectetur adipi- scing elit. Vivamus condimentum, massa eu accumsan pellentesque, felis metus imperdiet est, aliquam </div>
            <!-- EOF footer_widget text --> 
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- EOF Row second --> 
  <!-- EOF footer text --> <!-- Footer -->
  <div class="ambitios_footer_tile_g_left"></div>
  <div class="ambitios_footer_tile_g_right"></div>
  <div class="ambitios_footer">
    <div class="ambitios_container_16">
      <div class="ambitios_copy"> &copy; 2010 ambitious. All rights reserved. </div>
    </div>
  </div>
  <!-- EOF Footer --> 

</div>
    </form>
</body>
</html>
