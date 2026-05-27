-- =============================================
-- ShopEZ - Products Data Script
-- 30+ Products with Different Categories and Images
-- =============================================

USE ECommerceDb;
GO

-- Insert Products
INSERT INTO Products (Name, Description, Price, ImageUrl, Stock) VALUES
-- Electronics (1-10)
('MacBook Pro 14"', 'Apple M3 Pro chip, 18GB RAM, 512GB SSD. Powerful laptop for professionals.', 199999, 'https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=500', 15),
('iPhone 15 Pro', 'A17 Pro chip, 128GB, Titanium design. The most advanced iPhone ever.', 134900, 'https://images.unsplash.com/photo-1695048133142-1a20484d2569?w=500', 25),
('Samsung Galaxy S24 Ultra', 'Snapdragon 8 Gen 3, 12GB RAM, 256GB. Premium Android flagship.', 129999, 'https://images.unsplash.com/photo-1610945265064-0e34e5519bbf?w=500', 20),
('Sony WH-1000XM5', 'Industry-leading noise canceling headphones with 30-hour battery life.', 29990, 'https://images.unsplash.com/photo-1618366712010-f4ae9c647dcb?w=500', 30),
('Dell XPS 15', 'Intel Core i7, 16GB RAM, 512GB SSD, 4K OLED display.', 149999, 'https://images.unsplash.com/photo-1593642632559-0c6d3fc62b89?w=500', 12),
('iPad Pro 12.9"', 'M2 chip, 128GB, Liquid Retina XDR display. Ultimate tablet experience.', 119900, 'https://images.unsplash.com/photo-1544244015-0df4b3ffc6b0?w=500', 18),
('Apple Watch Series 9', 'GPS + Cellular, 45mm. Advanced health features and fitness tracking.', 44900, 'https://images.unsplash.com/photo-1546868871-7041f2a55e12?w=500', 35),
('Canon EOS R6 Mark II', '24.2MP full-frame mirrorless camera with 4K 60fps video.', 215990, 'https://images.unsplash.com/photo-1516035069371-29a1b244cc32?w=500', 8),
('Nintendo Switch OLED', '7-inch OLED screen, 64GB storage. Gaming on the go.', 34999, 'https://images.unsplash.com/photo-1578303512597-81e6cc155b3e?w=500', 40),
('GoPro Hero 12', '5.3K video, HyperSmooth 6.0 stabilization. Action camera.', 49990, 'https://images.unsplash.com/photo-1564466809058-bf4114d55352?w=500', 22),

-- Accessories (11-20)
('Anker PowerCore 26800', '26800mAh portable charger with PowerIQ technology.', 3999, 'https://images.unsplash.com/photo-1609091839311-d5365f9ff1c5?w=500', 50),
('Logitech MX Master 3S', 'Advanced wireless mouse with ergonomic design and silent clicks.', 9995, 'https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=500', 45),
('Samsung T7 Shield 2TB', 'Portable SSD with rugged design and USB 3.2 Gen 2.', 12999, 'https://images.unsplash.com/photo-1597872200969-2b65d56bd16b?w=500', 30),
('Apple MagSafe Charger', '15W wireless charging for iPhone 12 and later.', 4499, 'https://images.unsplash.com/photo-1586816879360-004f5b0c51e5?w=500', 60),
('JBL Flip 6', 'Portable Bluetooth speaker with powerful bass and 12-hour playtime.', 9999, 'https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=500', 55),
('SanDisk Extreme Pro 256GB', 'SDXC memory card with 170MB/s transfer speed.', 4999, 'https://images.unsplash.com/photo-1555664424-778a1e5e1b48?w=500', 70),
('Belkin 3-in-1 Charger', 'Wireless charging stand for iPhone, Apple Watch, and AirPods.', 7999, 'https://images.unsplash.com/photo-1615526675159-e248c3022d0f?w=500', 40),
('USB-C Hub 7-in-1', 'Multiport adapter with HDMI, USB 3.0, SD card reader.', 2499, 'https://images.unsplash.com/photo-1625723044792-44de16ccb4e9?w=500', 80),
('Anker Soundcore Space Q45', 'Hybrid active noise canceling headphones with 50-hour battery.', 12999, 'https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500', 35),
('Spigen Ultra Hybrid Case', 'Clear protective case for iPhone 15 with raised bezels.', 1499, 'https://images.unsplash.com/photo-1601784551446-20c9e07cdbdb?w=500', 100),

-- Peripherals (21-25)
('Keychron K2 Pro', 'Wireless mechanical keyboard with RGB backlight and hot-swappable switches.', 8999, 'https://images.unsplash.com/photo-1595225476474-87563907a212?w=500', 25),
('Razer DeathAdder V3', 'Ergonomic gaming mouse with 30K DPI optical sensor.', 5999, 'https://images.unsplash.com/photo-1527814050087-3793815479db?w=500', 40),
('LG UltraGear 27"', '27-inch IPS gaming monitor with 165Hz refresh rate and 1ms response.', 24999, 'https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=500', 20),
('Logitech C920 HD Pro', '1080p webcam with stereo audio and auto light correction.', 6995, 'https://images.unsplash.com/photo-1587826080692-f439cd0b70da?w=500', 50),
('Wacom Intuos Pro', 'Digital drawing tablet with 8192 pressure levels and tilt support.', 24999, 'https://images.unsplash.com/photo-1544967082-d9d25d867d66?w=500', 15),

-- Mobiles (26-30)
('OnePlus 12', 'Snapdragon 8 Gen 3, 16GB RAM, 256GB. Hasselblad camera system.', 64999, 'https://images.unsplash.com/photo-1598327105666-5b89351aff97?w=500', 28),
('Google Pixel 8 Pro', 'Tensor G3 chip, 12GB RAM, 128GB. Best Android camera.', 106999, 'https://images.unsplash.com/photo-1598327105666-5b89351aff97?w=500', 22),
('Xiaomi 14 Ultra', 'Leica camera system, Snapdragon 8 Gen 3, 16GB RAM, 512GB.', 79999, 'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=500', 18),
('Nothing Phone 2', 'Unique transparent design with Glyph interface and Snapdragon 8+ Gen 1.', 44999, 'https://images.unsplash.com/photo-1510557880182-3d4d3cba35a5?w=500', 25),
('Realme GT 5', 'Snapdragon 8 Gen 2, 240W fast charging, 16GB RAM, 256GB.', 39999, 'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=500', 30),

-- Smart Home (31-35)
('Amazon Echo Dot 5th Gen', 'Smart speaker with Alexa and improved audio.', 4499, 'https://images.unsplash.com/photo-1543512214-318c7553f230?w=500', 65),
('Google Nest Hub', '7-inch smart display with Google Assistant.', 7999, 'https://images.unsplash.com/photo-1558089687-f282ffcbc126?w=500', 35),
('Philips Hue Starter Kit', '4 smart color bulbs with bridge. Control lights with app.', 14999, 'https://images.unsplash.com/photo-1558089687-f282ffcbc126?w=500', 45),
('Ring Video Doorbell', 'HD video doorbell with motion detection and two-way audio.', 12999, 'https://images.unsplash.com/photo-1558002038-1055907df827?w=500', 40),
('TP-Link Smart Plug', 'WiFi-enabled smart plug with energy monitoring.', 1999, 'https://images.unsplash.com/photo-1558089687-f282ffcbc126?w=500', 90);

GO

PRINT 'Products inserted successfully!';
SELECT COUNT(*) AS 'Total Products' FROM Products;
GO
