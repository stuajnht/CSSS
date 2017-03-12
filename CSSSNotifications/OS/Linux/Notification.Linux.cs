﻿//  CSSS - CyberSecurity Scoring System Notifications
//  Copyright(C) 2017  Jonathan Hart (stuajnht) <stuajnht@users.noreply.github.com>
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program. If not, see <http://www.gnu.org/licenses/>.

using CSSSNotifications;
using System;

namespace OS.Linux
{
    internal class NotificationLinux : Notification
    {
        /// <summary>
        /// Displays a positive notification to the competitor to
        /// let them know points have been gained
        /// </summary>
        public override void PointsGained()
        {
            ShowNotification("Points Gained", "dialog-information");
        }

        /// <summary>
        /// Displays a nagative notification to the competitor to
        /// let them know points have been lost
        /// </summary>
        public override void PointsLost()
        {
            ShowNotification("Points Lost", "dialog-error");
        }

        /// <summary>
        /// Displays a neutral notification to the competitor to
        /// let them know points have been gained and lost
        /// </summary>
        public override void PointsChanged()
        {
            ShowNotification("Points Changed", "dialog-warning");
        }

        /// <summary>
        /// Shows the notification
        /// </summary>
        /// <param name="TitleText">The text to display in the notification title</param>
        /// <param name="NotificationIcon">The icon to show, see: https://specifications.freedesktop.org/icon-naming-spec/icon-naming-spec-latest.html</param>
        private void ShowNotification(string TitleText, string NotificationIcon)
        {
            var notificationParameters = "-t 5 \"" + TitleText + "\" --icon=" + NotificationIcon;

            try
            {
                // For Linux, the program is called notify-send (libnotify-bin package)
                System.Diagnostics.Process.Start("notify-send", notificationParameters);
            }
            catch (Exception e)
            {
                // It's probably not possible to get here, as trying
                // to display the notification without 'libnotify-bin'
                // installed shows a message of (at least for Debian):
                //   xdg-open: unexpected option 't'
                //   Try 'xdg-open --help' for more information
                logger.Debug("Exception thrown when trying to display desktop notification: {0}", e.Message);
                logger.Warn("Unable to show desktop notification. Is the 'libnotify-bin' package installed?");
            }
        }
    }
}
