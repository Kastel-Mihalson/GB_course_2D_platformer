using UnityEngine;

public class ContactsPoller
{
    private const float _collisionTresh = 0.1f;
    private const int _contactPointsCount = 10;

    private ContactPoint2D[] _contactPoints = new ContactPoint2D[_contactPointsCount];
    private Collider2D _collider;

    public bool IsGrounded { get; private set; }
    public bool HasLeftContacts { get; private set; }
    public bool HasRightContacts { get; private set; }

    public ContactsPoller(Collider2D collider)
    {
        _collider = collider;
    }

    public void Update()
    {
        IsGrounded = false;
        HasLeftContacts = false;
        HasRightContacts = false;

        var contactsCount = _collider.GetContacts(_contactPoints);

        for (int i = 0; i < contactsCount; i++)
        {
            var normal = _contactPoints[i].normal;

            if (normal.y > _collisionTresh) IsGrounded = true;
            if (normal.x > _collisionTresh) HasLeftContacts = true;
            if (normal.x < -_collisionTresh) HasRightContacts = true;
        }
    }
}
