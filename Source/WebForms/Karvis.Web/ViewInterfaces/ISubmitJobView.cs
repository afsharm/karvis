using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Core;

namespace Karvis.Web
{
    public interface ISubmitJobView : IView
    {
        /// <summary>
        /// User has pressed save/update button or any other action that
        /// indicates page must save information
        /// </summary>
        event EventHandler SaveUpdateButtonPressed;

        event EventHandler EditButtonPressed;

        event EventHandler NewButtonPressed;

        /// <summary>
        /// Show job info and fields on the page
        /// </summary>
        void ShowJob(Job job);

        /// <summary>
        /// read jobs/info from controls on the page
        /// </summary>
        Job ReadJob();

        /// <summary>
        /// is user logged in authorized as an admin?
        /// </summary>
        bool IsUserAuthorized();

        /// <summary>
        /// Is page is requested for edit mode
        /// </summary>
        bool IsInEditMode();

        /// <summary>
        /// Logged user have not enough access 
        /// </summary>
        void ForbidPage();

        /// <summary>
        /// some sections must not be shown public
        /// </summary>
        void DisableAutorizedSections();

        /// <summary>
        /// clears all fields
        /// </summary>
        void Clear();

        int GetEditingJobId();

        void Freez();

        void UnFreez();

        void DisableNewButton();
        void DisableSaveButton();
        void DisableEditButton();

        void EnableNewButton();
        void EnableSaveButton();
        void EnableEditButton();
    }
}
